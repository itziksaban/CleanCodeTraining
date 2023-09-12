namespace CleanCodeMiniTasks.Task3;

public class SecurityAlert
{
    public AlertType AlertType { get; set; }
    public string AlertMessage { get; set; } = default!;
    public DateTime AlertTime { get; set; }
}

public enum AlertType
{
    None,
    UnauthorizedAccess,
    MalwareDetected,
    ExcessiveFilesLocked
}

public class SecurityAlertHandler
{
    public void HandleSecurityAlert(SecurityAlert alert)
    {
        if (alert.AlertType == AlertType.UnauthorizedAccess)
        {
            HandleUnauthorizedAccess(alert);
        }
        else if (alert.AlertType == AlertType.MalwareDetected)
        {
            HandleMalwareDetected(alert);
        }
        else if (alert.AlertType == AlertType.ExcessiveFilesLocked)
        {
            HandleExcessiveFilesLocked(alert);
        }
        else
        {
            HandleUnknownAlertType(alert);
        }
    }

    private void HandleUnknownAlertType(SecurityAlert alert)
    {
        throw new NotImplementedException();
    }

    private void HandleExcessiveFilesLocked(SecurityAlert alert)
    {
        throw new NotImplementedException();
    }

    private void HandleMalwareDetected(SecurityAlert alert)
    {
        throw new NotImplementedException();
    }

    private void HandleUnauthorizedAccess(SecurityAlert alert)
    {
        throw new NotImplementedException();
    }
}

