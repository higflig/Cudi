using System;
using Discord;
using System.Threading.Tasks;
using Discord.Commands;


namespace Cubi.Modules
{
    public class MathCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Remainder")]
        [Alias("remainder")]
        [Summary("Finds the remainder 2 numbers.")]
        public async Task RemainderAsync(float num1 = 0, string symbol = null, float num2 = 0)
        {
            if (num1 != 0)
            {
                EmbedBuilder remainder = new EmbedBuilder();
                var answer = ($"{num1 % num2}");
                var equation = ($"{num1} ÷  {num2}");

                remainder.Build();
                remainder.AddField("Equation", equation);
                remainder.AddField("Remainder", answer);
                remainder.WithColor(Color.Blue);

                await Context.Channel.SendMessageAsync("", false, remainder.Build());
            }
            
            else
            {
                var band = new EmbedBuilder();

                band.WithTitle("Command: .remainder");
                band.WithDescription("**Description:** Finds the remainder of a divison equation\n**Usage:** .remainder [number] / [number]\n**Example:** .remainder 3 / 2");


                await Context.Channel.SendMessageAsync("", false, band.Build());
            }
        }

        [Command("Random")]
        [Alias("random", "randomnumber", "rn")]
        [Summary("Picks a random number between the two specified numbers")]
        public async Task Bll(int num1 = 0, [Remainder] int num2 = 0)
        {
            if (num1 != 0)
            {
                Random r = new Random();
                int answer = r.Next(num1, num2 + 1);


                EmbedBuilder t = new EmbedBuilder();

                t.Build();
                t.AddField("Parameters", $"{num1} to {num2}");
                t.AddField("Number", answer);
                t.WithColor(Color.Blue);

                await Context.Channel.SendMessageAsync("", false, t.Build());
            }
            
            else
            {
                var band = new EmbedBuilder();

                band.WithTitle("Command: .randomnumber");
                band.WithDescription("**Description:** Picks a random number within the range given\n**Usage:** .randomnumber [number] [number]\n**Example:** .randomnumber 1 10");


                await Context.Channel.SendMessageAsync("", false, band.Build());
            }
        }

        [Command("calculate")]
        [Alias("calc", "c")]
        public async Task CaclAsync(int num1 = 0, string symbol = null, int num2 = 0)
        {
            if (num1 != 0)
            {
                if (symbol == "+")
                {
                    var add = new EmbedBuilder();
                    var answer = ($"{num1 + num2}");
                    var equation = ($"{num1} + {num2}");

                    add.Build();
                    add.AddField("Equation", equation);
                    add.AddField("Answer", answer);
                    add.WithColor(Color.Blue);

                    await Context.Channel.SendMessageAsync("", false, add.Build());
                }
                if (symbol == "-")
                {
                    var subtract = new EmbedBuilder();
                    var answer = ($"{num1 - num2}");
                    var equation = ($"{num1} - {num2}");

                    subtract.Build();
                    subtract.AddField("Equation", equation);
                    subtract.AddField("Answer", answer);
                    subtract.WithColor(Color.Blue);

                    await Context.Channel.SendMessageAsync("", false, subtract.Build());
                }
                if (symbol == "x")
                {
                    var multiply = new EmbedBuilder();
                    var answer = ($"{num1 * num2}");
                    var equation = ($"{num1} x {num2}");

                    multiply.Build();
                    multiply.AddField("Equation", equation);
                    multiply.AddField("Answer", answer);
                    multiply.WithColor(Color.Blue);

                    await Context.Channel.SendMessageAsync("", false, multiply.Build());
                }
                if (symbol == "/")
                {
                    var divide = new EmbedBuilder();
                    var answer = ($"{(float)num1 / (float)num2}");
                    var equation = ($"{num1} ÷  {num2}");

                    divide.Build();
                    divide.AddField("Equation", equation);
                    divide.AddField("Answer", answer);
                    divide.WithColor(Color.Blue);

                    await Context.Channel.SendMessageAsync("", false, divide.Build());
                }
            }
            else
            {
                var band3 = new EmbedBuilder();

                band3.WithTitle("Command: .calc");
                band3.WithDescription("**Description:** Calculator\n**Usage:** .calc [number] [symbol] [number]\n**Example:** .calc 5 + 5");


                await Context.Channel.SendMessageAsync("", false, band3.Build());
            }
        }
    }
}
