using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using study.ApprovalSolution.OfficialDocument;

namespace study.ApprovalSolution.Client
{

  partial class OfficialDocumentCollectionActions
  {

    public override void Sign(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var comment = Functions.OfficialDocument.ShowCommentDialog(false, study.ApprovalSolution.OfficialDocuments.Resources.SignResult);
      if (comment != Constants.Docflow.OfficialDocument.CancelDialogResult)
      {
        foreach (var doc in _objs)
        {
          if (doc.HasVersions)
            base.Sign(e);
          
          Functions.OfficialDocument.SetComment(doc, comment, doc.Info.Properties.InternalApprovalState.GetLocalizedValue(OfficialDocument.InternalApprovalState.Signed));
          Functions.OfficialDocument.SetResult(doc, Constants.Docflow.OfficialDocument.SignResult);
          Functions.OfficialDocument.Remote.NextStep(doc);
        }
      }
    }

    public override bool CanSign(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _objs.All(d => d.InternalApprovalState == InternalApprovalState.PendingSign &&
                       d.TaskParticipants.Any(p => Equals(p.Executor, Sungero.Company.Employees.Current) && string.IsNullOrEmpty(p.Result)));

    }

    public virtual void ApproveResult(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var comment = Functions.OfficialDocument.ShowCommentDialog(false, study.ApprovalSolution.OfficialDocuments.Resources.ApproveResult);
      if (comment != Constants.Docflow.OfficialDocument.CancelDialogResult)
      {
        foreach (var doc in _objs)
        {
          Functions.OfficialDocument.SetComment(doc, comment, doc.Info.Properties.InternalApprovalState.GetLocalizedValue(OfficialDocument.InternalApprovalState.OnApproval));
          Functions.OfficialDocument.SetResult(doc, Constants.Docflow.OfficialDocument.ApproveResult);
          Functions.OfficialDocument.Remote.NextStep(doc);
        }
      }
    }

    public virtual bool CanApproveResult(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _objs.All(d => (d.InternalApprovalState == InternalApprovalState.ApproveBoss ||
                             d.InternalApprovalState == InternalApprovalState.OnApproval) &&
                       d.TaskParticipants.Any(p => Equals(p.Executor, Sungero.Company.Employees.Current) && string.IsNullOrEmpty(p.Result)));
    }


    public virtual bool CanStartApproval(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _objs.All(d => d.InternalApprovalState == null ||
                       d.InternalApprovalState == InternalApprovalState.Aborted);
    }

    public virtual void StartApproval(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var comment = Functions.OfficialDocument.ShowCommentDialog(false, study.ApprovalSolution.OfficialDocuments.Resources.NeedSignText);
      if (comment != Constants.Docflow.OfficialDocument.CancelDialogResult)
      {
        foreach (var doc in _objs)
        {
          Functions.OfficialDocument.SetComment(doc, comment, doc.Info.Properties.InternalApprovalState.GetLocalizedValue(OfficialDocument.InternalApprovalState.OnApproval));
          Functions.OfficialDocument.Remote.NextStep(doc);
        }
      }
    }

    public virtual bool CanFixedResult(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _objs.All(d => d.InternalApprovalState == InternalApprovalState.OnRework &&
                       d.TaskParticipants.Any(p => Equals(p.Executor, Sungero.Company.Employees.Current) && string.IsNullOrEmpty(p.Result)));
    }

    public virtual void FixedResult(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var comment = Functions.OfficialDocument.ShowCommentDialog(false, study.ApprovalSolution.OfficialDocuments.Resources.FixedResult);
      if (comment != Constants.Docflow.OfficialDocument.CancelDialogResult)
      {
        foreach (var doc in _objs)
        {
          Functions.OfficialDocument.SetComment(doc, comment, study.ApprovalSolution.OfficialDocuments.Resources.FixedText);
          Functions.OfficialDocument.SetResult(doc, Constants.Docflow.OfficialDocument.FixedResult);
          Functions.OfficialDocument.Remote.NextStep(doc);
        }
      }
    }
    
    public virtual bool CanReworkResult(Sungero.Domain.Client.CanExecuteActionArgs e)
    {
      return _objs.All(d => (d.InternalApprovalState == InternalApprovalState.ApproveBoss ||
                             d.InternalApprovalState == InternalApprovalState.OnApproval ||
                             d.InternalApprovalState == InternalApprovalState.PendingSign) &&
                       d.TaskParticipants.Any(p => Equals(p.Executor, Sungero.Company.Employees.Current) && string.IsNullOrEmpty(p.Result)));
    }

    public virtual void ReworkResult(Sungero.Domain.Client.ExecuteActionArgs e)
    {
      var comment = Functions.OfficialDocument.ShowCommentDialog(true, "");
      if (comment != Constants.Docflow.OfficialDocument.CancelDialogResult)
      {
        foreach (var doc in _objs)
        {
          Functions.OfficialDocument.SetComment(doc, comment, doc.Info.Properties.InternalApprovalState.GetLocalizedValue(OfficialDocument.InternalApprovalState.OnRework));
          Functions.OfficialDocument.SetResult(doc,  Constants.Docflow.OfficialDocument.ReworkResult);
          Functions.OfficialDocument.Remote.NextStep(doc);
        }
      }
    }
  }
}