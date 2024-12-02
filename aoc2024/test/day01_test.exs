defmodule Aoc2024Test.Day01Test do
  use ExUnit.Case

  import Aoc2024.Day01

  # Remove to run test
  # @tag :skip
  test "part1" do
    exInput = """
    3   4
    4   3
    2   5
    1   3
    3   9
    3   3
    """

    # input = nil
    result = part1(exInput)

    assert result
  end

  # Remove to run test
  # @tag :skip
  # test "part2" do
  #   input = nil
  #   result = part2(input)
  #
  #   assert result
  # end
end
