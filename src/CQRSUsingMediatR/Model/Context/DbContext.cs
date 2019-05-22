using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSUsingMediatR.Model.Context
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Conn { get; }
        IDbTransaction Transaction { get; }

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }

    public class DbContext : IDbContext
    {
        private IDbConnection _conn;
        private IDbTransaction _transaction;

        private readonly string _providerName;
        private readonly string _connectionString;

        public DbContext()
        {
            var server = @".\sqlexpress2008";
            var dbName = "DbRetail_XX01";
            var dbUser = "admin";
            var dbUserPass = "admin";

            _providerName = "System.Data.SqlClient";
            _connectionString = $"Server={server};Database={dbName};User Id={dbUser};Password={dbUserPass};";

            if (_conn == null) _conn = GetOpenConnection(_providerName, _connectionString);
        }

        public IDbConnection Conn
        {
            get { return _conn ?? (_conn = GetOpenConnection(_providerName, _connectionString)); }
        }

        private IDbConnection GetOpenConnection(string providerName, string connectionString)
        {
            IDbConnection conn = null;

            try
            {
                var provider = DbProviderFactories.GetFactory(providerName);

                conn = provider.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
            }
            catch
            {
            }

            return conn;
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_transaction == null) _transaction = _conn.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                try
                {
                    if (_conn.State != ConnectionState.Closed)
                    {
                        if (_transaction != null) _transaction.Rollback();

                        _conn.Close();
                    }
                }
                finally
                {
                    _conn.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
