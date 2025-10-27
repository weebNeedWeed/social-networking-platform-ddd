using System.Data;
using Microsoft.Extensions.Configuration;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Npgsql;

namespace Modules.IAM.Infrastructure.Persistence;

public class IAMUnitOfWork : IIAMUnitOfWork, IDisposable
{
    private IUserAccountRepository? _userAccountRepository;

    private IDbConnection? _connection; 
    private IDbTransaction? _transaction;
    private bool _disposed = false;

    public IAMUnitOfWork(IConfiguration configuration)
    {
        var iamConnectionString = configuration.GetConnectionString("IAM")!;
        _connection = new NpgsqlConnection(iamConnectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public IUserAccountRepository UserAccountRepository =>
        _userAccountRepository is not null ? _userAccountRepository : new UserAccountRespository(_transaction!);

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = _connection?.BeginTransaction();
            ResetRepositories();
        }
    }

    private void ResetRepositories()
    {
        _userAccountRepository = null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_transaction is not null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }

                if (_connection is not null)
                {
                    _connection.Dispose();
                    _connection = null;
                }

                _disposed = true;
            }
        }
    }

    ~IAMUnitOfWork()
    {
        Dispose(false);
    }
}