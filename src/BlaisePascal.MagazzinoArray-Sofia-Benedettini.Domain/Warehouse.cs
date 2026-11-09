namespace BlaisePascal.MagazzinoArray_Sofia_Benedettini.Domain
{
    public class Warehouse
    {
        private int[,] _boxesQuantity;
        private int[][] _articlesCodes;

        private int _floorsNumber;

        public int FloorsNumber
        {
            get { return _floorsNumber; }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The warehouse must have a positive number of floors");
                }

                _floorsNumber = value;
            }
        }

        private int _corridorsNumber;

        public int CorridorsNumber
        {
            get { return _corridorsNumber; }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The warehouse must have a positive number of corridors");
                }

                _corridorsNumber = value;
            }
        }

        public Warehouse(int floorsNum, int corridorsNum)
        {
            FloorsNumber = floorsNum;
            CorridorsNumber = corridorsNum;

            _boxesQuantity = new int[floorsNum, corridorsNum];
            _articlesCodes = new int[floorsNum][];

            for (int i = 0; i < floorsNum; i += 1)
            {
                _articlesCodes[i] = new int[1];
            }
        }

   

        private void ValidateFloor(int floorToAccess)
        {
            if (floorToAccess < 0 || floorToAccess >= FloorsNumber)
            {
                throw new ArgumentException("The floor entered is not valid");
            }
        }

        private void ValidateCorridor(int corridorToAccess)
        {
            if (corridorToAccess < 0 || corridorToAccess >= CorridorsNumber)
            {
                throw new ArgumentException("The corridor entered is not valid");
            }
        }

        private void ValidateProductId(int idOfProduct)
        {
            if (idOfProduct < 10000 || idOfProduct > 99999)
            {
                throw new ArgumentException("The id of the product must have 5 numbers");
            }
        }

        private int CountArticles(int floorToAccess)
        {
            int counter = 0;

            for (int i = 0; i < _articlesCodes[floorToAccess].Length; i += 1)
            {
                if (_articlesCodes[floorToAccess][i] != 0)
                {
                    counter += 1;
                }
            }

            return counter;
        }

        private int FindArticlePosition(int floorToAccess, int idOfProduct)
        {
            int articlesCounter = CountArticles(floorToAccess);

            for (int i = 0; i < articlesCounter; i += 1)
            {
                if (_articlesCodes[floorToAccess][i] == idOfProduct)
                {
                    return i;
                }
            }

            return -1;
        }

        private void ResizeArticlesArray(int floorToAccess)
        {
            int newSize = _articlesCodes[floorToAccess].Length * 2;

            int[] newArticlesCodes = new int[newSize];

            for (int i = 0; i < _articlesCodes[floorToAccess].Length; i += 1)
            {
                newArticlesCodes[i] = _articlesCodes[floorToAccess][i];
            }

            _articlesCodes[floorToAccess] = newArticlesCodes;
        }


        public void AddBoxes(int floorToAccess, int corridorToAccess, int boxesToAdd)
        {
            ValidateFloor(floorToAccess);
            ValidateCorridor(corridorToAccess);

            if (boxesToAdd <= 0)
            {
                throw new ArgumentException("The number of boxes to add must be positive");
            }

            _boxesQuantity[floorToAccess, corridorToAccess] += boxesToAdd;
        }

        public void DecreaseBoxes(int floorToAccess, int corridorToAccess, int boxesToRemove)
        {
            ValidateFloor(floorToAccess);
            ValidateCorridor(corridorToAccess);

            if (boxesToRemove <= 0)
            {
                throw new ArgumentException("The number of boxes to remove must be positive");
            }

            if (_boxesQuantity[floorToAccess, corridorToAccess] < boxesToRemove)
            {
                throw new ArgumentException("Impossible to remove more boxes than their number");
            }

            _boxesQuantity[floorToAccess, corridorToAccess] -= boxesToRemove;
        }

        public void ProductStorage(int floorToAccess, int idOfProduct)
        {
            ValidateFloor(floorToAccess);
            ValidateProductId(idOfProduct);

            int counter = CountArticles(floorToAccess);

            if (counter == _articlesCodes[floorToAccess].Length)
            {
                ResizeArticlesArray(floorToAccess);
            }

            _articlesCodes[floorToAccess][counter] = idOfProduct;
        }

        public void RemoveProductFromFloor(int floorToAccess, int idOfProduct)
        {
            ValidateFloor(floorToAccess);
            ValidateProductId(idOfProduct);

            int positionOfId = FindArticlePosition(floorToAccess, idOfProduct);

            if (positionOfId == -1)
            {
                throw new ArgumentException("The product does not exist");
            }

            int counter = CountArticles(floorToAccess);

            for (int i = positionOfId; i < counter - 1; i += 1)
            {
                _articlesCodes[floorToAccess][i] =
                    _articlesCodes[floorToAccess][i + 1];
            }

            _articlesCodes[floorToAccess][counter - 1] = 0;
        }

        public int FindMoreCrowded()
        {
            int maxQuantity = -1;
            int floorWithMaxQuantity = 0;

            for (int i = 0; i < FloorsNumber; i += 1)
            {
                int totalQuantity = 0;

                for (int j = 0; j < CorridorsNumber; j += 1)
                {
                    totalQuantity += _boxesQuantity[i, j];
                }

                if (totalQuantity > maxQuantity)
                {
                    maxQuantity = totalQuantity;
                    floorWithMaxQuantity = i;
                }
            }

            return floorWithMaxQuantity;
        }

        public int[] SearchProduct(int idOfProduct)
        {
            ValidateProductId(idOfProduct);

            int[] floorsFound = new int[FloorsNumber];
            int counter = 0;

            for (int i = 0; i < FloorsNumber; i += 1)
            {
                int positionOfId = FindArticlePosition(i, idOfProduct);

                if (positionOfId != -1)
                {
                    floorsFound[counter] = i;
                    counter += 1;
                }
            }

            int[] newFloorsFound = new int[counter];

            for (int i = 0; i < counter; i += 1)
            {
                newFloorsFound[i] = floorsFound[i];
            }

            return newFloorsFound;
        }
    }
}