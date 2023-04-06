namespace index_editor_app
{
    public partial class Form1
    {
        int editingImageIndex = -1;
        bool ImageSystemEditing = true;

        public async Task InitializeImagesTabAsync()
        {
            InitializePageImagesComboBox();
            InitializeImageLists();
        }

        public void InitializeImageLists()
        {
            // Set image sizes
            MemberImageList.ImageSize = new Size(160, 160);
            NewsImageList.ImageSize = new Size(160, 160);
            SpecialtiesImageList.ImageSize = new Size(160, 160);
            EventsImageList.ImageSize = new Size(160, 160);
            ResourcesImageList.ImageSize = new Size(160, 160);

            // Set image color (this fixes poor image quality)
            MemberImageList.ColorDepth = ColorDepth.Depth16Bit;
            NewsImageList.ColorDepth = ColorDepth.Depth16Bit;
            SpecialtiesImageList.ColorDepth = ColorDepth.Depth16Bit;
            EventsImageList.ColorDepth = ColorDepth.Depth16Bit;
            ResourcesImageList.ColorDepth = ColorDepth.Depth16Bit;
        }

        public void InitializePageImagesComboBox()
        {
            foreach (string name in imageHandler.GetImagePageNames())
            {
                PageImagesComboBox.Items.Add(name);
            }
        }

        public void InitializeImageDataGrid(ImageList imageList, List<string> imageLinks)
        {
            this.ImageDataGridView.CancelEdit();
            this.ImageDataGridView.Columns.Clear();
            this.ImageDataGridView.Rows.Clear();
            this.ImageDataGridView.DataSource = null;

            // Add "Image" column
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "Image";
            imageColumn.HeaderText = "Image";
            //imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageColumn.Width = 160;
            this.ImageDataGridView.Columns.Add(imageColumn);

            // Add "Link" column
            DataGridViewTextBoxColumn linkColumn = new DataGridViewTextBoxColumn();
            linkColumn.Name = "Link";
            linkColumn.HeaderText = "Image Link";
            linkColumn.Width = 300;
            this.ImageDataGridView.Columns.Add(linkColumn);

            // Add "In Use" column
            DataGridViewTextBoxColumn inUseColumn = new DataGridViewTextBoxColumn();
            inUseColumn.Name = "InUse";
            inUseColumn.HeaderText = "In Use";
            inUseColumn.Width = 80;
            this.ImageDataGridView.Columns.Add(inUseColumn);

            this.ImageDataGridView.Rows.Add(imageList.Images.Count);

            // Set the row height for each row
            foreach (DataGridViewRow row in this.ImageDataGridView.Rows)
            {
                row.Height = 160;
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            for (int i = 0; i < imageList.Images.Count; i++)
            {
                this.ImageDataGridView.Rows[i].Cells["Image"].Value = imageList.Images[i];
                this.ImageDataGridView.Rows[i].Cells["Link"].Value = imageLinks[i];

                bool isUsed = imageHandler.UrlIsUsed(imageLinks[i]);
                if (isUsed)
                {
                    this.ImageDataGridView.Rows[i].Cells["InUse"].Value = "Yes";
                    this.ImageDataGridView.Rows[i].Cells["InUse"].Style.BackColor = Color.Green;
                }
                else
                {
                    this.ImageDataGridView.Rows[i].Cells["InUse"].Value = "No";
                    this.ImageDataGridView.Rows[i].Cells["InUse"].Style.BackColor = Color.Red;
                }
            }

            // Add edit button
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            ImageDataGridView.Columns.Add(btn);
            btn.Width = 60;
            btn.HeaderText = "Edit";//column header
            btn.Text = "Edit";
            btn.Name = "Edit";
            btn.UseColumnTextForButtonValue = true;
        }

        private void PageImagesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageImagesComboBox.SelectedItem != null)
            {
                string propertyName = PageImagesComboBox.SelectedItem.ToString();
                List<string> imageLinks = new List<string>();

                switch (propertyName)
                {
                    case "Events":
                        imageLinks = imageHandler.GetEventImageLinks();
                        DownloadImagesFromWeb(imageLinks, EventsImageList);
                        InitializeImageDataGrid(EventsImageList, imageLinks);
                        PageNameLabel.Text = "Events";
                        break;
                    case "Members":
                        imageLinks = imageHandler.GetMemberImageLinks();
                        DownloadImagesFromWeb(imageLinks, MemberImageList);
                        InitializeImageDataGrid(MemberImageList, imageLinks);
                        PageNameLabel.Text = "Members";
                        break;
                    case "News":
                        imageLinks = imageHandler.GetNewsImageLinks();
                        DownloadImagesFromWeb(imageLinks, NewsImageList);
                        InitializeImageDataGrid(NewsImageList, imageLinks);
                        PageNameLabel.Text = "News";
                        break;
                    case "Specialties":
                        imageLinks = imageHandler.GetSpecialtyImageLinks();
                        DownloadImagesFromWeb(imageLinks, SpecialtiesImageList);
                        InitializeImageDataGrid(SpecialtiesImageList, imageLinks);
                        PageNameLabel.Text = "Specialties";
                        break;
                    case "Resources":
                        imageLinks = imageHandler.GetResourcesImageLinks();
                        DownloadImagesFromWeb(imageLinks, ResourcesImageList);
                        InitializeImageDataGrid(ResourcesImageList, imageLinks);
                        PageNameLabel.Text = "Resources";
                        break;
                }
            }
        }

        private void ImageDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in ImageDataGridView.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            //column 3 = edit button
            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                editingImageIndex = e.RowIndex;
                //highlight the column being edited
                ImageDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                //load image data
            }
        }

        private void DeleteImageButton_Click(object sender, EventArgs e)
        {

        }

        private void DownloadImagesFromWeb(List<string> address, ImageList il)
        {
            il.Images.Clear();
            foreach (string img in address)
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create(img);
                System.Net.WebResponse resp = request.GetResponse();
                System.IO.Stream respStream = resp.GetResponseStream();
                Bitmap bmp = new Bitmap(respStream);
                respStream.Dispose();
                il.Images.Add(bmp);
            }
        }
    }
}



