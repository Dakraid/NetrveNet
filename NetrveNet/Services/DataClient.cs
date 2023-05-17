using Squidex.ClientLibrary;
using Squidex.ClientLibrary.ServiceExtensions;

namespace NetrveNet.Services
{
    public class DataClient
    {
        private readonly IConfiguration _config;
        private readonly ILogger<DataClient> _logger;
        private readonly ISquidexClient _squidexClient;

        public DataClient(IConfiguration config, ILogger<DataClient> logger, ISquidexClientProvider squidexClientProvider)
        {
            _config = config;
            _logger = logger;
            _squidexClient = squidexClientProvider.Get();
        }
    }
}
