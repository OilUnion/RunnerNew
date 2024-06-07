namespace Runner {
  internal sealed class Field {

    private readonly Int32[,] map;
    private readonly Int32 rows;
    private readonly Int32 cols;

    internal Field(Int32 rows, Int32 cols) {
      this.rows = rows;
      this.cols = cols;
      this.map = new Int32[rows, cols];
    }

    internal Field(Int32 rows, Int32 cols, Int32[,] field) {
      this.rows = rows;
      this.cols = cols;
      this.map = field;
    }

    internal Field FillingFieldWithZeros() {
      var map = this.map;
      for (var i = 0; i < this.rows; i++) {
        for (var j = 0; j < this.cols; j++) {
          map[i, j] = 0;
        }
      }
      return new Field(this.rows, this.cols, map);
    }

    internal Field MarkingOfCompletedPathByPlayer(Player player) {
      Int32[,] map = this.map;
      Int32 startPosX = this.rows - 1; // С последней строки.
      Int32 startPosY = this.cols / 2; // С середины поля.
      map[startPosX, startPosY] = 1;
      while (startPosX != 0) {
        (Int32 currenPosX, Int32 currenPosY) = player.Move(startPosX, startPosY);
        if (this.CheckingForGoingOutOfBounds(currenPosX, currenPosY)
           || this.IsReturningZeroValueOfPartOfField(currenPosX, currenPosY)) {
          continue;
        }
        map[startPosX = currenPosX, startPosY = currenPosY] = 1;
      }
      return new Field(this.rows, this.cols, map);
    }


    private Boolean CheckingForGoingOutOfBounds(Int32 currenPosX, Int32 currenPosY) {
      return !(0 <= currenPosX
               && currenPosX < this.rows
                 && 0 <= currenPosY
                   && currenPosY < this.cols);
    }

    private Boolean IsReturningZeroValueOfPartOfField(Int32 x, Int32 y) 
      => this.map[x, y] != 0;


    internal void Show() {
      for (var i = 0; i < this.rows; i++) {
        for (var j = 0; j < this.cols; j++) {
          Console.ForegroundColor = this.map[i, j] == 1
                                    ? ConsoleColor.Green
                                    : ConsoleColor.White;
          Console.Write(this.map[i, j].ToString() + " ");
        }
        Console.WriteLine();
      }
    }
  }
}