defmodule Aoc2024.Day01 do

  def part1(input) do
    list =
      Enum.map(String.split(input, "\n", [trim: true]), fn string -> string end)
      |> Enum.map(String.split(" ", [trim: true]), fn string -> string end)
      |> IO.inspect()

    {left, right} =
      list
      |> Enum.reduce({[], []}, fn value, {list1, list2} ->
        a = String.to_integer(Enum.at(value, 0))
        b = String.to_integer(Enum.at(value, 1))
      {[a | list1], [b | list2]}
      end)

      left_storted = Enum.sort(left)
      right_sorted = Enum.sort(right)
  end

  # def part2(args) do
  # end
end
