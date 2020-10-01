using AmbulanceDatabase.Context;
using AmbulanceSystem.Shared.Config;

namespace AmbulanceSystem.Shared.OperationStatusHandling
{
    public enum AlertType
    {
        Success, Error
    }

    public enum OperationType
    {
        Create, Edit, Delete, Details
    }

    public class OperationStatus
    {
        public string Message { get; set; }
        public AlertType AlertType { get; set; }

        public OperationStatus(string message, AlertType alertType)
        {
            Message = message;
            AlertType = alertType;
        }
    }

    public static class StatusHandler
    {
        public static OperationStatus Handle(OperationType operation, DbStatus status)
        {
            switch (operation)
            {
                case OperationType.Create: return HandleCreateOperation(status);
                case OperationType.Edit: return HandleEditOperation(status);
                case OperationType.Delete: return HandleDeleteOperation(status);
                default: return new OperationStatus(string.Empty, AlertType.Error);
            }
        }

        private static OperationStatus HandleCreateOperation(DbStatus status)
        {
            switch (status)
            {
                case DbStatus.SUCCESS: return new OperationStatus(language.AddingSuccess, AlertType.Success);
                case DbStatus.EXISTS: return new OperationStatus(language.EntityExists, AlertType.Error);
                case DbStatus.DATABASE_ERROR: return new OperationStatus(language.DatabaseError, AlertType.Error);
                default: return new OperationStatus(language.DatabaseError, AlertType.Error);
            }
        }

        private static OperationStatus HandleEditOperation(DbStatus status)
        {
            switch (status)
            {
                case DbStatus.SUCCESS: return new OperationStatus(language.UpdatingSuccess, AlertType.Success);
                case DbStatus.NOT_FOUND: return new OperationStatus(language.EntityNotFound, AlertType.Error);
                case DbStatus.DATABASE_ERROR: return new OperationStatus(language.DatabaseError, AlertType.Error);
                default: return new OperationStatus(language.DatabaseError, AlertType.Error);
            }
        }

        private static OperationStatus HandleDeleteOperation(DbStatus status)
        {
            switch (status)
            {
                case DbStatus.SUCCESS: return new OperationStatus(language.DeletingSuccess, AlertType.Success);
                case DbStatus.NOT_FOUND: return new OperationStatus(language.EntityNotFound, AlertType.Error);
                case DbStatus.DATABASE_ERROR: return new OperationStatus(language.DatabaseError, AlertType.Error);
                default: return new OperationStatus(language.DatabaseError, AlertType.Error);
            }
        }
    }
}
