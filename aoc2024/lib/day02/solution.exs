defmodule Aoc2024.Day02.Solution do
  def read_input(file_path) do
    File.stream!(file_path)
    |> Stream.map(&String.trim/1)
    |> Stream.map(&String.split(&1, " ", trim: true))
    |> Stream.map(fn list -> Enum.map(list, &String.to_integer/1) end)
    |> Enum.to_list()
  end

  def is_safe(report) do
    inc = Enum.at(report, 0) < Enum.at(report, 1)

    Enum.zip(report, tl(report))
    |> Enum.all?(fn {a, b} ->
      diff = abs(a - b)
      diff != 0 && diff <= 3 && a < b == inc
    end)
  end

  def part_one(file_path) do
    read_input(file_path)
    |> Enum.count(&is_safe/1)
    |> IO.inspect()
  end

  def part_two(file_path) do
    read_input(file_path)
    |> Enum.count(fn report ->
      is_safe(report) ||
        Enum.any?(0..(length(report) - 1), fn index ->
          is_safe(List.delete_at(report, index))
        end)
    end)
    |> IO.inspect()
  end
end

# Aoc2024.Day02.Solution.part_one("lib/day02/input")
Aoc2024.Day02.Solution.part_two("lib/day02/input")
