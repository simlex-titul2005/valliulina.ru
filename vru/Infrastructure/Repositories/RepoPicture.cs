using SX.WebCore.Repositories;

namespace vru.Infrastructure.Repositories
{
    public sealed class RepoPicture : SxRepoPicture
    {
        public RepoPicture()
        {
            InsertNotFreePictures = (sb) => {
                sb.AppendLine(" INSERT INTO @result SELECT de.PictureId FROM D_EDUCATION AS de WHERE de.PictureId IS NOT NULL ");
            };
        }
    }
}