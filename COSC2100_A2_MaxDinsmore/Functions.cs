using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSC2100_A2_MaxDinsmore
{
    class Functions
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

        // I originally overcomplicated this, only need to check for win based off ring piece that was
        // just placed

        public static int[][] checkForWin(Tile[] tiles, int gridSize, int[] placedPiece)
        {
            int[][] check = [[-1]];

            // 3D orthogonal Check
            if (check != checkForXOrthogonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize))
            {
                return checkForXOrthogonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize);
            }

            if (check != checkForYOrthogonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize))
            {
                return checkForYOrthogonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize);
            }

            if (check != checkForZOrthogonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize))
            {
                return checkForZOrthogonalWin(tiles, placedPiece[0], placedPiece[1], placedPiece[2], gridSize);
            }
            // 2D diagonal Checks



            // 3D Diagonal Checks



            // No one won
            return check;
        }

        private static int[][] checkForZOrthogonalWin(Tile[] tiles, int x, int y, int z, int gridSize)
        {
            try
            {
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + x + gridSize].getRingValue(z - 1) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y + x * gridSize].getRingValue(z - 2))
                {
                    return [[x, y, z], [x, y, z - 1], [x, y, z -2]];
                }
            }
            catch { }
            try
            {
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + x * gridSize].getRingValue(z + 1) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y + x * gridSize].getRingValue(z + 2))
                {
                    return [[x, y, z], [x, y, z + 1], [x, y, z + 2]];
                }
                
            }
            catch { }
            try
            {
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + x * gridSize].getRingValue(z - 1) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y + x * gridSize].getRingValue(z + 1))
                {
                    return [[x, y, z - 1], [x, y, z], [x, y, z + 1]];
                }
            }
            catch { }
            // No one won
            return [[-1]];
        }
        private static int[][] checkForYOrthogonalWin(Tile[] tiles, int x, int y, int z, int gridSize)
        {
            try
            { // Y to Y down 2
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y - 1 + x * gridSize].getRingValue(z) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y - 2 + x * gridSize].getRingValue(z))
                {
                    return [[x, y, z], [x, y-1, z], [x, y-2, z]];
                }
            }
            catch { }
            try
            { // Y to Y up 2
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + 1 + x * gridSize].getRingValue(z) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y + 2 + x * gridSize].getRingValue(z))
                {
                    return [[x, y , z], [x, y + 1, z], [x, y + 2, z]];
                }
            }
            catch { }

            try
            {
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + 1 + x * gridSize].getRingValue(z) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y - 1 + x * gridSize].getRingValue(z))
                {
                    return [[x, y - 1, z], [x, y, z], [x, y + 1, z]];
                }
            }
            catch { }
            // No one won
            return [[-1]];
        }
        private static int[][] checkForXOrthogonalWin(Tile[] tiles, int x, int y, int z, int gridSize)
        {
            try
            {
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + (x - 1) * gridSize].getRingValue(z) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y + (x - 2)* gridSize].getRingValue(z))
                {
                    return [[x, y, z], [x - 1, y, z], [x - 2, y, z]];
                }
            }
            catch { }

            try
            {
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + (x + 1) * gridSize].getRingValue(z) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y + (x + 2) * gridSize].getRingValue(z))
                {
                    return [[x, y, z], [x, y, z], [x, y, z]];
                }
            }
            catch { }

            try
            {
                if (tiles[y + x * gridSize].getRingValue(z) == tiles[y + (x - 1) * gridSize].getRingValue(z) &&
                    tiles[y + x * gridSize].getRingValue(z) == tiles[y + (x + 1) * gridSize].getRingValue(z))
                {
                    return [[x - 1, y, z], [x, y, z], [x + 1, y, z]];
                }
            }
            catch { }

            // No one won
            return [[-1]];
        }


    }
}

// Spare code for more complicated way to solve

//for (int x = 0; x < gridSize; x++)
//{
//    for (int y = 0; y < gridSize; y++)
//    {       
//        for (int z = 0; z < AmountOfRings; z++)
//        {   // IF Y == Z specific checks
//            if (z == y && tiles[x + y * gridSize].getRingValue(z) != 0)
//            { // Checks for win in Z direction
//                if (check != checkForZOrthogonalWin(tiles, x, y, z, gridSize))
//                {
//                    return checkForZOrthogonalWin(tiles, x, y, z, gridSize);
//                }
//                if (check != checkForYOrthogonalWin(tiles, x, y, z, gridSize))
//                {
//                    return checkForYOrthogonalWin(tiles, x, y, z, gridSize);
//                }
//            }
//            if (y == x && tiles[x + y * gridSize].getRingValue(z) != 0)
//            {
//                if (check != checkForXOrthogonalWin(tiles, x, y, z, gridSize))
//                {
//                    return checkForXOrthogonalWin(tiles, x, y, z, gridSize);
//                }
//                if (check != checkForYOrthogonalWin(tiles, x, y, z, gridSize))
//                {
//                    return checkForYOrthogonalWin(tiles, x, y, z, gridSize);
//                }
//            }
//            if (z == x && tiles[x + y * gridSize].getRingValue(z) != 0)
//            {
//                if (check != checkForXOrthogonalWin(tiles, x, y, z, gridSize))
//                {
//                    return checkForXOrthogonalWin(tiles, x, y, z, gridSize);
//                }
//                if (check != checkForZOrthogonalWin(tiles, x, y, z, gridSize))
//                {
//                    return checkForZOrthogonalWin(tiles, x, y, z, gridSize);
//                }
//            }
//        }

//    }
//}