using System;
using System.Reflection.Metadata.Ecma335;

namespace Runner {
  internal class Program {
    private static void Main() {
      Console.Write("Введите кол-во строк: ");
      _ = Int32.TryParse(Console.ReadLine(), out var rows);
      Console.WriteLine();
      Console.Write("Введите кол-во столбцов: ");
      _ = Int32.TryParse(Console.ReadLine(), out var cols);

      var field = new Field(rows, cols);
      var walking = new Walking(field);
      Random random = new Random();

      field.Show();
      walking.Move();
      Console.WriteLine();
      field.Show();

    }
  }

  internal sealed class Field {

    private readonly Int32[,] field;
    internal Int32 Rows { get; private set; }
    internal Int32 Cols { get; private set; }

    internal Field(Int32 rows, Int32 cols) {
      this.Rows = rows;
      this.Cols = cols;
      this.field = this.CreatingPlayingField(rows, cols);
      this.FillingFieldWithZeros(rows, cols);
    }

    private Int32[,] CreatingPlayingField(Int32 rows, Int32 cols) {
      return new Int32[rows, cols];
    }

    private void FillingFieldWithZeros(Int32 rows, Int32 cols) {
      for (var i = 0; i < this.Rows; i++) {
        for (var j = 0; j < this.Cols; j++) {
          this.field[i, j] = 0;
        }
      }
    }

    internal void Show() {
      for (var i = 0; i < this.Rows; i++) {
        for (var j = 0; j < this.Cols; j++) {
          if (this.field[i, j] == 1) {
            Console.ForegroundColor = ConsoleColor.Green;
          }
          Console.Write(this.field[i, j].ToString() + " ");
          Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
      }
    }

    internal void Mark(Int32 x, Int32 y) {
      if (x < this.Rows
            && 0 <= x
              && y < this.Cols
                && 0 <= y) {
        this.field[x, y] = 1;
      }
    }

    internal Int32 ReturningValueOfPartOfField(Int32 x, Int32 y) {
      if (x < this.Rows
            && 0 <= x
              && y < this.Cols
                && 0 <= y) {
        return this.field[x, y];
      }
      return 1;
    }
  }

  internal sealed class Walking {
    private readonly Field field;
    private Int32 startPosX;
    private Int32 startPosY;

    public Walking(Field field) {
      this.field = field;
      this.SetStartPosition();
    }

    private void SetStartPosition() {
      this.startPosX = this.field.Rows - 1;
      this.startPosY = this.field.Cols / 2;
    }

    internal void Move() {
      var random = new Random();
      this.field.Mark(this.startPosX, this.startPosY);

      var currenPosX = this.startPosX;
      var currenPosY = this.startPosY;

      while (currenPosX != 0) {
        var num = random.Next(-1, 2);

        var probCurrenPosX = currenPosX;
        var probCurrenPosY = currenPosY;

        this.MakeMove(num, ref probCurrenPosX, ref probCurrenPosY);

        if (this.CheckingForGoingOutOfBounds(probCurrenPosX, probCurrenPosY, this.field)) {
          continue;
        }

        if (this.field.ReturningValueOfPartOfField(probCurrenPosX, probCurrenPosY) == 0) {
          currenPosX = probCurrenPosX;
          currenPosY = probCurrenPosY;

          this.field.Mark(currenPosX, currenPosY);
        }
      }
    }

    private void MakeMove(Int32 num, ref Int32 currenPosX, ref Int32 currenPosY) {
      switch (num) {
        case -1:
          currenPosY += num;
          break;
        case 0:
          currenPosX += -1;
          break;
        case 1:
          currenPosY += num;
          break;
        default:
          break;
      }
    }

    // Проверка на приделы поля.
    private Boolean CheckingForGoingOutOfBounds(Int32 currenPosX
                                                , Int32 currenPosY
                                                , Field field) {
      return !(currenPosX <= field.Rows
               && 0 <= currenPosX
                 && currenPosY <= field.Cols
                   && 0 <= currenPosY);
    }
  }
}
