defmodule Aoc2024.Day03.Solution do
  def read_input(file_path) do
    File.read!(file_path)
  end

  def part_one(file_path) do
    regex = ~r/mul\((\d+),(\d+)\)/

    read_input(file_path)
    |> then(&Regex.scan(regex, &1))
    |> Enum.map(fn [_, first, second] ->
      first_val = String.to_integer(first)
      second_val = String.to_integer(second)
      first_val * second_val
    end)
    |> Enum.sum()
    |> IO.inspect()
  end

  def extract_between(input, start_keyword, end_keyword, content_pattern) do
    input
    |> String.split(start_keyword)
    |> Enum.drop(1)
    |> Enum.map(&String.split(&1, end_keyword))
    |> Enum.map(&List.first/1)
    |> Enum.flat_map(fn content ->
      Regex.scan(content_pattern, content || "")
      |> Enum.map(fn [_, a, b] ->
        String.to_integer(a) * String.to_integer(b)
      end)
    end)
    |> Enum.sum()
  end

  def part_two(file_path) do
    file = read_input(file_path)

    start_keyword = "do()"
    end_keyword = "don't()"
    mul_regex = ~r/mul\((\d+),(\d+)\)/

    first_keyword_pos =
      case Regex.run(~r/do\(\)|don't/, file, return: :index) do
        nil -> String.length(file)
        [{pos, _}] -> pos
      end

    before_section = String.slice(file, 0, first_keyword_pos)

    muls_before_keyword =
      Regex.scan(mul_regex, before_section)
      |> Enum.map(fn [_, a, b] ->
        String.to_integer(a) * String.to_integer(b)
      end)
      |> Enum.sum()

    matches =
      Aoc2024.Day03.Solution.extract_between(file, start_keyword, end_keyword, mul_regex)

    (matches + muls_before_keyword)
    |> IO.inspect()
  end
end

# Aoc2024.Day03.Solution.part_one("lib/day03/input")
Aoc2024.Day03.Solution.part_two("lib/day03/input")
