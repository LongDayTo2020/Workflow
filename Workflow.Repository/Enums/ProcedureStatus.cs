using System.ComponentModel;

namespace Workflow.Repository.Enums;

public class ProcedureStatus
{
    /// <summary>
    /// 待審核
    /// </summary>
    [Description("待審核")] public const string WaitReview = "WaitReview";

    /// <summary>
    /// 同意
    /// </summary>
    [Description("同意")] public const string Approval = "Approval";

    /// <summary>
    /// 拒絕
    /// </summary>
    [Description("拒絕")] public const string Reject = "Reject";

    /// <summary>
    /// 完成
    /// </summary>
    [Description("完成")] public const string Finish = "Finish";
    
    /// <summary>
    /// 下一步
    /// </summary>
    [Description("下一步")] public const string NextStep = "NextStep";
}