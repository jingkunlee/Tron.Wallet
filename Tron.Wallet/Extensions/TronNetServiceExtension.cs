using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tron.Wallet {
    public static class TronNetServiceExtension {
        public static IServiceCollection AddTronNet(this IServiceCollection services, Action<TronNetOptions> setupAction) {
            var options = new TronNetOptions();

            setupAction(options);

            services.AddTransient<ITransactionClient, TransactionClient>();
            services.AddTransient<IGrpcChannelClient, GrpcChannelClient>();
            services.AddTransient<ITronClient, TronClient>();
            services.AddTransient<IWalletClient, WalletClient>();
            services.AddSingleton<Contracts.IContractClientFactory, Contracts.ContractClientFactory>();
            services.AddTransient<Contracts.TRC20ContractClient>();
            services.Configure(setupAction);

            return services;
        }
    }
}
