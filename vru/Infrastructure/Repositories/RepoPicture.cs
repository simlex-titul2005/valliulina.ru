using SX.WebCore.SxRepositories;
using System;
using System.Text;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoPicture : SxRepoPicture
    {
        protected override Action<StringBuilder> InsertNotFreePictures
        {
            get
            {
                return sb => {
                    sb.AppendLine(" INSERT INTO @result SELECT de.PictureId FROM D_EDUCATION AS de WHERE de.PictureId IS NOT NULL ");
                };
            }
        }
    }
}