﻿using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Fuel_Bot.Commands;
using Newtonsoft.Json;

namespace Fuel_Bot
{
	class Bot
	{
		public DiscordClient Client { get; private set; }
		public CommandsNextModule Commands { get; private set; }
		public async Task RunAsync()
		{
			var json = string.Empty;

			using (var fs = File.OpenRead("config.json"))
			using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
				json = await sr.ReadToEndAsync().ConfigureAwait(false);

			var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

				var config = new DiscordConfiguration
				{
					Token = configJson.Token,
					TokenType = TokenType.Bot,
					AutoReconnect = true,
					LogLevel = LogLevel.Debug,
					UseInternalLogHandler = true
				};

			Client = new DiscordClient(config);

			Client.Ready += OnClientReady;

			var commandsConfig = new CommandsNextConfiguration
			{
				StringPrefix = new string(configJson.Prefix),
				EnableDms = false,
				EnableMentionPrefix = true,
				IgnoreExtraArguments = true
			};

			Commands = Client.UseCommandsNext(commandsConfig);

			Commands.RegisterCommands<FunCommands>();

			await Client.ConnectAsync();

			await Task.Delay(-1);
		}
		private Task OnClientReady(ReadyEventArgs e)
		{
			return Task.CompletedTask;
		}
	}
}
