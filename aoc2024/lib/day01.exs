defmodule Aoc2024.Day01 do
  def read_input(file_path) do
    File.stream!(file_path)
    |> Stream.map(&String.trim/1)
    |> Stream.map(&String.split(&1, " ", trim: true))
    |> Enum.to_list()
  end

  def split_values(file) do
    {left, right} =
      file
      |> Enum.reduce({[], []}, fn value, {list1, list2} ->
        a = String.to_integer(List.first(value))
        b = String.to_integer(List.last(value))
        {[a | list1], [b | list2]}
      end)

    left_storted = Enum.sort(left)
    right_sorted = Enum.sort(right)
    {left_storted, right_sorted}
  end

  def part1(file_path) do
    {left_sorted, right_sorted} = split_values(read_input(file_path))

    Enum.zip(left_sorted, right_sorted)
    |> Enum.map(fn {a, b} -> abs(a - b) end)
    |> Enum.sum()
    |> IO.inspect()
  end

  def part2(file_path) do
    {left_sorted, right_sorted} = split_values(read_input(file_path))

    left_sorted
    |> Enum.map(fn value ->
      count = Enum.count(right_sorted, fn y -> y == value end)
      {value, count}
    end)
    |> Enum.map(fn {value, count} ->
      value * count
    end)
    |> Enum.sum()
    |> IO.inspect()
  end
end

# Aoc2024.Day01.part1()
Aoc2024.Day01.part2("lib/day01.input")
