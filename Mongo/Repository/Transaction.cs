using Domain.IRepository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Repository
{
    public class Transaction : ITransaction, IDisposable
    {
        private readonly IMongoClient _mongoClient;

        private IClientSessionHandle _session;

        public object CurrentSession => _session;

        public Transaction(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _session = InitializeSession();
        }

        private IClientSessionHandle InitializeSession()
        {
            IClientSessionHandle clientSessionHandle = _mongoClient.StartSession();
            clientSessionHandle.StartTransaction(new TransactionOptions(ReadConcern.Majority, ReadPreference.Primary, WriteConcern.WMajority));
            return clientSessionHandle;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _session.CommitTransactionAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _session.AbortTransactionAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            _session?.Dispose();
        }

    }
}
