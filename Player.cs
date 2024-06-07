namespace Runner {
  internal sealed class Player {

    public Player() { }

    internal (Int32, Int32) Move(Int32 startPosX, Int32 startPosY) {
      var num = new Random().Next(-1, 2);
      return this.MakeMove(num, startPosX, startPosY);
    }

    private (Int32, Int32) MakeMove(Int32 num, Int32 currenPosX, Int32 currenPosY) {
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
      return (currenPosX, currenPosY);
    }
  }
}