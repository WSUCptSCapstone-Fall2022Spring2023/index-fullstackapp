using index_editor_app_engine.JsonClasses;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace index_editor_app_engine
{
    public class ImageHandler
    {
        public IndexImageList imageList;
        private IndexAPIClient indexClient;
        List<string> imagesUsed;
        public ImageHandler(IndexAPIClient indexAPIClient, string imageJson)
        {
            imagesUsed = new List<string>();
            indexClient = indexAPIClient;
            this.imageList = JsonConvert.DeserializeObject<IndexImageList>(imageJson);
        }

        public async Task<MemoryStream> GetImageAsync(string location)
        {
            PathType type = GetPathType(location);

            if (type == PathType.OsPath)
            {
                return GetLocalImage(location);
            }
            else if (type == PathType.Url)
            {
                return await GetApiImageAsync(location);
            }
            else
            {
                return null;
            }
        }
        public async Task<MemoryStream> GetApiImageAsync(string url)
        {
            byte[] image = await indexClient.GetImageAsync(url);
            MemoryStream ms = new MemoryStream(image, 0, image.Length);
            return ms;
        }

        public MemoryStream GetLocalImage(string path)
        {
            byte[] image = File.ReadAllBytes(path);
            MemoryStream ms = new MemoryStream(image, 0, image.Length);
            return ms;
        }

        public void UploadMemberImages(MembersPage memberspage)
        {
            foreach (BoardMember b in memberspage.BoardMembers)
            {
                PathType type = GetPathType(b.Image);

                if (type == PathType.OsPath)
                {
                    byte[] myImageFile = GetFileByteArray(b.Image);
                    var imageBinaryContent = new ByteArrayContent(myImageFile);
                    imageBinaryContent.Headers.Add("Content-Type", "image/png");
                    string dirName = new DirectoryInfo(b.Image).Name;
                    string imageEndpoint = "https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/" + "memberimages" + "/" + dirName;
                   
                    b.Image = "https://index-webapp.s3.amazonaws.com/img/" + "memberimages" + "/" + dirName;
                    imageList.Members.Add(b.Image);

                    //put the image
                    indexClient.PutImageAsync(imageEndpoint, imageBinaryContent);

                    // update image list
                    UploadImageList();
                }
            }
        }

        //   "https://index-webapp.s3.amazonaws.com/img/newsimages/FromTheEditor.png",
        public void UploadNewsImages(NewsPage newsPage)
        {
            foreach (NewsItem n in newsPage.NewsItems)
            {
                PathType type = GetPathType(n.Image);

                if (type == PathType.OsPath)
                {
                    byte[] myImageFile = GetFileByteArray(n.Image);
                    var imageBinaryContent = new ByteArrayContent(myImageFile);
                    imageBinaryContent.Headers.Add("Content-Type", "image/png");
                    string dirName = new DirectoryInfo(n.Image).Name;
                    string imageEndpoint = "https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/" + "newsimages" + "/" + dirName;
                    n.Image = "https://index-webapp.s3.amazonaws.com/img/" + "newsimages" + "/" + dirName;

                    imageList.News.Add(n.Image);

                    //put the image
                    indexClient.PutImageAsync(imageEndpoint, imageBinaryContent);

                    // update image list
                    UploadImageList();
                }
            }
        }

        public void UploadEventImages(EventsPage eventsPage)
        {
            foreach (Event e in eventsPage.Events)
            {
                PathType type = GetPathType(e.Image);

                if (type == PathType.OsPath)
                {
                    byte[] myImageFile = GetFileByteArray(e.Image);
                    var imageBinaryContent = new ByteArrayContent(myImageFile);
                    imageBinaryContent.Headers.Add("Content-Type", "image/png");
                    string dirName = new DirectoryInfo(e.Image).Name;
                    string imageEndpoint = "https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/" + "eventimages" + "/" + dirName;

                    e.Image = "https://index-webapp.s3.amazonaws.com/img/" + "eventimages" + "/" + dirName;
                    imageList.Events.Add(e.Image);

                    //put the image
                    indexClient.PutImageAsync(imageEndpoint, imageBinaryContent);

                    // update image list
                    UploadImageList();
                }
            }
        }

        public void UploadSpecialtyImages(Specialties specialtyPage)
        {
            foreach (Specialty s in specialtyPage.SpecialtiesList)
            {
                PathType type = GetPathType(s.Image);

                if (type == PathType.OsPath)
                {
                    byte[] myImageFile = GetFileByteArray(s.Image);
                    var imageBinaryContent = new ByteArrayContent(myImageFile);
                    imageBinaryContent.Headers.Add("Content-Type", "image/png");
                    string dirName = new DirectoryInfo(s.Image).Name;
                    string imageEndpoint = "https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/" + "specialtyimages" + "/" + dirName;
                    s.Image = "https://index-webapp.s3.amazonaws.com/img/" + "specialtyimages" + "/" + dirName;

                    imageList.Specialties.Add(s.Image);
                    //put the image
                    indexClient.PutImageAsync(imageEndpoint, imageBinaryContent);

                    // update image list
                    UploadImageList();
                }
            }
        }

        public void UploadResourceImages(ResourcesPage resourcesPage)
        {
            foreach (Resource r in resourcesPage.Resources)
            {
                PathType type = GetPathType(r.PageImage);

                if (type == PathType.OsPath)
                {
                    byte[] myImageFile = GetFileByteArray(r.PageImage);
                    var imageBinaryContent = new ByteArrayContent(myImageFile);
                    imageBinaryContent.Headers.Add("Content-Type", "image/png");
                    string dirName = new DirectoryInfo(r.PageImage).Name;
                    string imageEndpoint = "https://bz682vosnb.execute-api.us-east-1.amazonaws.com/dev1/img/" + "resourceimages" + "/" + dirName;
                    r.PageImage = "https://index-webapp.s3.amazonaws.com/img/" + "resourceimages" + "/" + dirName;
                    imageList.Resources.Add(r.PageImage);

                    //put the image
                    indexClient.PutImageAsync(imageEndpoint, imageBinaryContent);

                    // update image list
                    UploadImageList();
                }
            }
        }

        public void UploadImageList()
        {
            string updatedimageList = JsonConvert.SerializeObject(imageList);
            indexClient.PutDocument(updatedimageList, "indeximages");
        }

        public List<string> GetImagePageNames()
        {
            return imageList.GetPropertyNames();
        }

        public List<string> GetEventImageLinks()
        {
            return imageList.Events;
        }

        public List<string> GetMemberImageLinks()
        {
            return imageList.Members;
        }

        public List<string> GetNewsImageLinks()
        {
            return imageList.News;
        }

        public List<string> GetSpecialtyImageLinks()
        {
            return imageList.Specialties;
        }

        public List<string> GetResourcesImageLinks()
        {
            return imageList.Resources;
        }


        //check to see if the image is used on the website or not
        public bool UrlIsUsed(string url)
        {
            if (imagesUsed.Contains(url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddImagesUsed(List<string> urls)
        {
            imagesUsed.AddRange(urls);
        }

        public enum PathType
        {
            Url,
            OsPath,
            Unknown
        }

        public static PathType GetPathType(string path)
        {
            // Regex pattern for URLs
            string urlPattern = @"^(http|https):\/\/[a-zA-Z0-9\-_]+(\.[a-zA-Z0-9\-_]+)+([\S\s]*)$";

            // Regex pattern for OS paths
            string osPathPattern = @"^(?:[a-zA-Z]\:|\\\\[\w\s\d\.]+\\[\w\s\d\$]+)\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*$";

            if (Regex.IsMatch(path, urlPattern))
            {
                return PathType.Url;
            }
            else if (Regex.IsMatch(path, osPathPattern))
            {
                return PathType.OsPath;
            }
            else
            {
                return PathType.Unknown;
            }
        }
        static byte[] GetFileByteArray(string filename)
        {
            FileStream oFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file size.
            byte[] FileByteArrayData = new byte[oFileStream.Length];

            //Read file in bytes from stream into the byte array
            oFileStream.Read(FileByteArrayData, 0, System.Convert.ToInt32(oFileStream.Length));

            //Close the File Stream
            oFileStream.Close();

            return FileByteArrayData; //return the byte data
        }
    }
}
