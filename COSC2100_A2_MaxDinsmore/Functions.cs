// Class to hold spare
// Functions for 3D tic tac toe
// mainly used to store checking for wins Functions

namespace COSC2100_A2_MaxDinsmore
{
    static class Functions
    {
        // Function to check if user won after they placed piece
        //
        // Works by scanning grid and checks for any lines or
        // single tile wins i.e. all big medium and small ring all
        // placed on one tile.
        // Uses the tile ring information to check is user Won
        // 
        // Treat as a 3 dimensional array where third dimension is
        // the single tile.
        // 
        // Example tile circles array would be [0,0,2]
        // where 0 is never clicked and 2 is player 2's ring.
        // 
        // Returns array of where the 3 pieces are on the board
        //
        // Note for the rules of the same 3 in a row only wins
        // if sizes are all the same (i.e. 3 big rings in a diagonal)
        // or big to small / small to big, (i.e. small, medium, big in bottom row)
        // so 3 main checks
        // orthgonal check, diagonal along z,y check and diagonal along x,y,z check



        /// <summary>
        /// Calls other Checking for Win functions and returns their grid values if they are
        /// not the default check values
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="gridSize"></param>
        /// <param name="placedPiece"></param>
        /// <returns></returns>
        public static int[,] checkForWin(Tile[,] tiles, int gridSize, int[] placedPiece)
        {
            int[,] check = { { -3, -3, -3 }, {-3,-3,-3 }, { -3, -3, -3 } };

            // 3D orthogonal Check
            if (check[0,0] != orthogonalChecks(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize)[0,0])
            {
                return orthogonalChecks(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize);
            }


            // 2D diagonal Checks
            if (check[0,0] != checkFor2DDiagonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize)[0,0])
            {
                return checkFor2DDiagonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize);
            }


            // 3D Diagonal Checks
            if (check[0,0] != checkFor3DDiagonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize)[0,0])
            {
                return checkFor3DDiagonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize);
            }


            // No one won
            return check;
        }
        private static int[,] orthogonalChecks(Tile[,] tiles, int x, int y, int z, int gridSize)
        {
            int[,] orthogonalCheck = { { -3, -3, -3 }, { -3, -3, -3 }, { -3, -3, -3 } };
            for (int i = -1; i< 2; i+=2)
            {
                if (orthogonalCheck[0,0] == -3)
                {
                    // X Change
                    orthogonalCheck = checkForAxis(tiles, x, y, z, gridSize, i, 0, 0);
                }
                if (orthogonalCheck[0,0] == -3)
                {
                    // Y Change
                    orthogonalCheck = checkForAxis(tiles, x, y, z, gridSize, 0, i, 0);
                }
                if (orthogonalCheck[0,0] == -3)
                {
                    // Z Change
                    orthogonalCheck = checkForAxis(tiles, x, y, z, gridSize, 0, 0, i);
                }
                
            }

            return orthogonalCheck;
  
        }
        

        private static int[,] checkFor2DDiagonalWin(Tile[,] tiles, int x, int y, int z, int gridSize)
        {
            // Need +/+, +/-, -/+, -/- and 3 different times for YZ, ZX, YX
            // So can generalize the Axis change and then run 3 times for the different combinations
            // i.e. for loop the specifies combination change, and four if statements handling directions.
            int[] axisMovement = [1, 1, 0];
            int[,] twoDimensionalCheck;
            // I and J are representative of ++, +-.. Change
            // if i = 0 change is negative, 

            for (int axisChange = 0; axisChange < 3; axisChange++)
            {
                // Represents --, +-, ++, -+ changes
                // via 2 loops positive and negative change in the axis'
                for (int i = -1; i < 2; i += 2)
                {
                    for (int j = -1; j < 2; j += 2)
                    {
                        if (axisMovement[0] != 0)
                        {
                            axisMovement[0] = i;
                            if (axisMovement[1] != 0)
                            {
                                axisMovement[1] = j;
                            }
                            else
                            {
                                axisMovement[2] = j;
                            }

                        }
                        else if (axisMovement[1] != 0)
                        {
                            axisMovement[1] = i;
                            axisMovement[2] = j;
                        }

                        // lines got too long so saved value in twoDimensionalCheck
                        twoDimensionalCheck = checkForAxis(tiles, x, y, z, gridSize, axisMovement[0], axisMovement[1], axisMovement[2]);
                        if (twoDimensionalCheck[0,0].ToString() != "-3")
                        {
                            return twoDimensionalCheck;
                        }
                    }
                }
                // Represents the changes between YZ, XZ, YX
                if (axisMovement[0] != 0 && axisMovement[1] != 0)
                {
                    axisMovement[2] = 1;
                    axisMovement[0] = 0;
                }
                else if (axisMovement[1] != 0 && axisMovement[2] != 0)
                {
                    axisMovement[0] = 1;
                    axisMovement[1] = 0;
                }

            }

            return new int[,] { { -3, -3, -3 }, { -3, -3, -3 }, { -3, -3, -3 } };
        }




        private static int[,] checkFor3DDiagonalWin(Tile[,] tiles, int x, int y, int z, int gridSize)
        {
            // Need to check combinations of +- for all 3 axes
            // ---, -+-, -++, --+, +--, ++-, +-+, +++ 8 In total
            // can do like 2d and have 3 for loops
            int[,] threeDimensionalCheck;

            for (int i = -1; i < 2; i+=2)
            {
                for (int j = -1; j < 2; j += 2)
                {
                    for (int k = -1; k < 2; k +=2)
                    {
                        threeDimensionalCheck = checkForAxis(tiles, x, y, z, gridSize, i, j, k);
                        if (threeDimensionalCheck[0,0].ToString() != "-3")
                        {
                            return threeDimensionalCheck;
                        }
                    }
                }
            }

            return new int[,] { { -3, -3, -3 }, { -3, -3, -3 }, { -3, -3, -3 } };
        }


        /// <summary>
        ///  Does one check of a diagonal line on the grid to see if the player has won
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="gridSize"></param>
        /// <param name="xChange"></param>
        /// <param name="yChange"></param>
        /// <param name="zChange"></param>
        /// <returns></returns>
        private static int[,] checkForAxis(Tile[,] tiles, int x, int y, int z, int gridSize, int xChange, int yChange, int zChange)
        {
            // X & Y 
            //
            // Positive Change X and Y
            try
            {
                if (tiles[x,y].getRingValue(z) == tiles[x + xChange , y + yChange].getRingValue(z + zChange) &&
                    tiles[x,y].getRingValue(z) == tiles[x + xChange * 2 , y + yChange * 2].getRingValue(z + zChange * 2))
                {
                    return new int[,] { { x, y, z }, { x + xChange, y + yChange, z + zChange }, { x + xChange * 2, y + yChange * 2, z + zChange * 2 } };
                }
            }
            catch { }
            try
            {
                if (tiles[x, y].getRingValue(z) == tiles[x + xChange, y + yChange].getRingValue(z + zChange) &&
                    tiles[x, y].getRingValue(z) == tiles[x - xChange, y - yChange].getRingValue(z - zChange))
                {
                    return new int[,] { { x - xChange, y - yChange, z - zChange }, {x, y, z}, { x + xChange, y + yChange, z + zChange } };
                }
            }
            catch { }

            return new int[,] { { -3, -3, -3 }, { -3,-3,-3 }, { -3, -3, -3 } };
        }
    }



}

