using System.Data;
using System.Runtime.Loader;
using Npgsql;
using Workflow.Repository.Implement;
using Workflow.Repository.Interface;
using Workflow.Service.Implement;
using Workflow.Service.Interface;
using Workflow.StaticMethod;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnection = builder.Configuration.GetValue<string?>("ConnectionStrings:npgsql");
builder.Services.AddScoped<IDbConnection>(provider => new NpgsqlConnection(dbConnection));

#region DI Register

builder.Services.AddScoped<IWorkflowApprovalRepository, WorkflowApprovalRepository>();
builder.Services.AddScoped<IWorkflowRecordFileRepository, WorkflowRecordFileRepository>();
builder.Services.AddScoped<IWorkflowRecordRepository, WorkflowRecordRepository>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IWorkflowStepRepository, WorkflowStepRepository>();

builder.Services.AddScoped<IWorkflowStepService, WorkflowStepService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();
builder.Services.AddScoped<IApplicationProcedureService, ApplicationProcedureService>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();