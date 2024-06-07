namespace Runner {
  internal class Program {
    private static void Main() {
      Console.Write("Введите кол-во строк: ");
      _ = Int32.TryParse(Console.ReadLine(), out var rows);
      Console.WriteLine();
      Console.Write("Введите кол-во столбцов: ");
      _ = Int32.TryParse(Console.ReadLine(), out var cols);
      new Field(rows, cols)
        .FillingFieldWithZeros()
        .MarkingOfCompletedPathByPlayer(new Player())
        .Show();
    }
  }
}