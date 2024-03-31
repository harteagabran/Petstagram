using Petstagram.Models;

namespace Petstagram.Services
{
    public class StructureService
    {
        public List<Picture> SelectionSortByDate(List<Picture> pictureList)
        {
            int amt = pictureList.Count;
            for (int i = 0; i < amt - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < amt; j++)
                {
                    if (pictureList[j].UploadDate < pictureList[min].UploadDate)
                    {
                        min = j;
                    }
                }
                if(min != i)
                {
                    Picture temp = pictureList[i];
                    pictureList[i] = pictureList[min];
                    pictureList[min] = temp;
                }
            }
            return pictureList;
        }
    }
}
