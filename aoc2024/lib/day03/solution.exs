defmodule Aoc2024.Day03.Solution do
  def read_input(file_path) do
    File.stream!(file_path)
    |> Stream.map(&String.trim/1)
    |> Stream.map(&String.split(&1, " ", trim: true))
    |> Stream.map(fn list -> Enum.map(list, &String.to_integer/1) end)
    |> Enum.to_list()
  end

  def part_one(file_path) do
    nil
  end

  def part_two(file_path) do
    nil
  end
end

Aoc2024.Day03.Solution.part_one("lib/day03/input_test")
# Aoc2024.Day03.Solution.part_two("lib/day03/input")
