defmodule Aoc2024.Day02.Solution do
  def read_input(file_path) do
    File.stream!(file_path)
    |> Stream.map(&String.trim/1)
    |> Stream.map(&String.split(&1, " ", trim: true))
    |> Enum.to_list()
  end

  def is_safe_report(report) do
    pairs =
      Enum.chunk_every(report, 2, 1, :discard)
      |> Enum.map(fn [curr, next] ->
        curr_val = String.to_integer(curr)
        next_val = String.to_integer(next)
        diff = next_val - curr_val

        increment =
          cond do
            next_val > curr_val -> :increasing
            next_val < curr_val -> :decreasing
            true -> :equal
          end

        increment_val =
          cond do
            abs(diff) <= 3 -> :safe_range
            true -> :unsafe_range
          end

        {increment, increment_val}
      end)

    all_inc = Enum.all?(pairs, fn {increment, _} -> increment == :increasing end)
    all_dec = Enum.all?(pairs, fn {increment, _} -> increment == :decreasing end)
    all_safe = Enum.all?(pairs, fn {_, increment_val} -> increment_val == :safe_range end)

    cond do
      all_inc and all_safe -> 1
      all_dec and all_safe -> 1
      true -> 0
    end
  end

  def part1(file_path) do
    read_input(file_path)
    |> Enum.map(fn report ->
      is_safe_report(report)
    end)
    |> Enum.sum()
    |> IO.inspect()
  end

  def part2(file_path) do
    # TODO: need to to the same safety test
    # If a report is unsafe, the remove a value from the list and re-test
    # Iterate through the list to see if it's safe with any value removed
    # If so, mark safe
    # else mark unsafe

    nil
  end
end

Aoc2024.Day02.Solution.part1("lib/day02/input")
# Aoc2024.Day02.Solution.part2("lib/day02/input_test")
