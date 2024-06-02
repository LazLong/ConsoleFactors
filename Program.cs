// ConsoleFactors
// A simple command line tool to brute force the factors for a number with optional maximum value to use for the factors.
// Designed to save time when playing Beltmatic.
// TODO Finish adding Exponential support

//    Copyright(C) 2024 Brian Chappell

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using ConsoleFactors;

if (args is ["-h"] || args.Length == 0)
{
    Console.WriteLine("ConsoleFactors [long to factor] [highest available value]");
    return 0;
}

if (args.Length <= 2 && long.TryParse(args[0], out var target))
{
    var highestAvailableValue = 9L;
    if (args.Length == 2 && long.TryParse(args[1], out var highestValueParam)) highestAvailableValue = highestValueParam;
    Console.WriteLine($"Factoring {target} with values up to {highestAvailableValue}...");
    var result = new List<Phrase>();

    // Take target and get first factors
    // if only 1 and target, it's prime and, we have to consider an addition or subtraction
    // if either factor is greater than maximum available value, we need to factor it as above.

    var targetPhrase = new Phrase(target);

    var factors = GetFactors(targetPhrase, highestAvailableValue);
    
    Console.WriteLine($"Lowest value factor = {factors.ToString()}");

    

    return 0;
}
else
{
    if (args.Length > 2)
    {
        Console.WriteLine("Invalid number of parameters supplied.");
        return -1;
    }
    if (!long.TryParse(args[1], out long result))
    {
        Console.WriteLine("First parameter isn't an integer.");
        return -1;
    }
    Console.WriteLine($"Unknown error - args = {string.Join(",", args)}");
    return -1;
}

Phrase GetFactors(Phrase target, long maxValue)
{
    var calculations = new List<Phrase>();
    for (var add = -maxValue; add <= maxValue; add++)
    {
        for (var i = 2; i <= (long)Math.Floor(Math.Sqrt((double)target.Sum+add)); i++)
        {
            if ((target.Sum+add) % i != 0) continue;
            var factor = (target.Sum+add) / i;
            var calc = new Phrase(Math.Min(i, factor), Math.Max(i, factor), -add);
            if (!calculations.Contains(calc)) calculations.Add(calc);
        }
    }

    var result = calculations.OrderBy(a => a.Value).First();

    if (result.A > maxValue)
    {
        var param1 = new Phrase(result.A);
        result.Param1 = GetFactors(param1, maxValue);
    }

    if (result.B > maxValue)
    {
        var param2 = new Phrase(result.B);
        result.Param2 = GetFactors(param2, maxValue);
    }

    return result;
}

List<Phrase> GetExponents(long target, long maximumAvailableValue)
{
    var result = new List<Phrase>();
    for (var i = 2; i <= maximumAvailableValue; i++)
    {
        for (var j = 3; j <= maximumAvailableValue; j++)
        {
            var value = (long)Math.Pow(i, j);
            if (value <= (long)Math.Floor((decimal)target / 2))
            {
                result.Add(new Phrase(i, j, value));
            }
            else
            {
                break;
            }
        }
    }

    return result;
}