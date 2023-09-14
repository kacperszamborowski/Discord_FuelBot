using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace FuelBot.Commands
{
	public class Commands
	{
		[Command("fuel")]
		[Description("Calculate how much each of you has to pay for fuel\n use: !fuel *kilometers* *fuel_consumption* *price* *amount_of_passengers*")]
		public async Task Add(CommandContext ctx, 
			[Description("How many kilometers")] int km, [Description("Fuel consumption l/100km")] int consumption, [Description("Fuel price")] int price, [Description("Amount of passengers")] int passengers)
		{
			await ctx.Channel
				.SendMessageAsync("Km: " + km + "km\n" + 
				"Fuel consumption: " + consumption + "l/100km\n" + 
				"Fuel price: " + price + "€/l\n" + 
				"Amount of passengers: " + passengers + " passengers\n" + 
				"Each of you has to pay " +(((km/100 * consumption) * price) / passengers).ToString() + "€")
				.ConfigureAwait(false);
		}
	}
}
