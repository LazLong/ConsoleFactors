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

using System.Text;

namespace ConsoleFactors;

public class Phrase
{
    private long _a;
    private long _b;
    public long C = 0;
    public Phrase? Param1;
    public Phrase? Param2;

    public Phrase(long a, long b, long c = 0)
    {
        A = a;
        B = b;
        C = c;
    }

    public Phrase(long a)
    {
        A = a;
        B = 1;
        C = 0;
    }

    public long A
    {
        get => Param1?.Sum ?? _a;
        set => _a = value;
    }

    public long B
    {
        get => Param2?.Sum ?? _b;
        set => _b = value;
    }

    public long Sum => (C == 0) ? A * B : A * B + C;
    public long Value => (C == 0) ? A + B : (A + B + Math.Abs(C)) * 2;
    public override string ToString()
    {
        var result = new StringBuilder();

        result.Append(Param1 != null ? Param1.C == 0 ? $"{Param1}" : $"({Param1})" : A);
        result.Append(" * ");
        result.Append(Param2 != null ? Param2.C == 0 ? $"{Param2}" : $"({Param2})" : B);
        if (C != 0) result.Append($"{(C < 0 ? " - " : " + ")}{Math.Abs(C)}");

        return result.ToString();
    }
}
