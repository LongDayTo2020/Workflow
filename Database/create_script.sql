create table workflows
(
    id          integer generated by default as identity
        primary key,
    name        varchar(255) not null,
    create_time timestamp    not null,
    create_user varchar(255) not null,
    update_time timestamp,
    update_user varchar(255)
);

comment on table workflows is '流程基本表';

alter table workflows
    owner to postgres;

create table workflow_steps
(
    id          integer generated by default as identity,
    workflow_id integer      not null
        references workflows,
    step        integer      not null,
    final       integer      not null,
    name        varchar(255) not null,
    create_time timestamp    not null,
    create_user varchar(255) not null,
    update_time timestamp,
    update_user varchar(255),
    primary key (id, workflow_id)
);

comment on table workflow_steps is '流程步驟表';

comment on column workflow_steps.step is '步驟';

comment on column workflow_steps.final is '最後一步';

alter table workflow_steps
    owner to postgres;

create table workflow_records
(
    id              integer generated by default as identity
        primary key,
    workflow_id     integer           not null
        references workflows,
    priority        integer default 2 not null,
    expiration_time timestamp         not null,
    status          varchar(255),
    create_time     timestamp         not null,
    create_user     varchar(255)      not null,
    update_time     timestamp,
    update_user     varchar(255)
);

comment on table workflow_records is '流程紀錄表';

comment on column workflow_records.priority is '優先級';

comment on column workflow_records.expiration_time is '截止日';

comment on column workflow_records.status is '狀態';

alter table workflow_records
    owner to postgres;

create table workflow_approvals
(
    workflow_record_id integer      not null
        references workflow_records,
    workflow_step_id   integer      not null,
    approver           varchar(255) not null,
    description        varchar(255),
    approval_time      timestamp,
    expiration_time    timestamp    not null,
    status             varchar(255),
    create_time        timestamp    not null,
    create_user        varchar(255) not null,
    update_time        timestamp,
    update_user        varchar(255),
    primary key (workflow_record_id, workflow_step_id)
);

comment on table workflow_approvals is '流程審核表';

comment on column workflow_approvals.approver is '審核者';

comment on column workflow_approvals.description is '描述';

comment on column workflow_approvals.approval_time is '審核日';

comment on column workflow_approvals.expiration_time is '審核截止日';

comment on column workflow_approvals.status is '狀態';

alter table workflow_approvals
    owner to postgres;

create table workflow_record_files
(
    id                 integer generated by default as identity,
    workflow_record_id integer      not null
        references workflow_records,
    workflow_step_id   integer      not null,
    name               varchar(255) not null,
    show_name          varchar(255) not null,
    type               varchar(255) not null,
    length             bigint       not null,
    location           varchar(255) not null,
    description        varchar(255),
    create_time        timestamp    not null,
    create_user        varchar(255) not null,
    update_time        timestamp,
    update_user        varchar(255),
    primary key (id, workflow_record_id, workflow_step_id)
);

comment on table workflow_record_files is '流程審核表';

comment on column workflow_record_files.description is '描述';

alter table workflow_record_files
    owner to postgres;

