; $(document).ready(function () {
    var defaults = {
        clsNames: {
            PageUploadedImgDetail: '.page-uploaded-img-detail',
            UploadedImgList: '.uploaded-img-list',
            ImgUploadModalOpener: '.img-upload-modal-opener',
            PreviewListItemRemove: '.preview-list-item-remove',
            ModalImgUpload: '.modal-img-upload'
        },
        tmplNames: {
            UploadedImgListItem: 'tmpl-uploaded-img-list-item'
        },
        addImagePath: '/Newscast/CreateCroppedImage'
    };

    var modalAddImageCallback = function (data) {
        var $pageUploadedImgDetail = $(defaults.clsNames.PageUploadedImgDetail);
        var imgUploadDetailArray = getImgUploadDetailArray($pageUploadedImgDetail);
        $.post(defaults.addImagePath, data, function (result) {
            if (result.success) {
                imgUploadDetailArray.push(result.data);
                $pageUploadedImgDetail.val(JSON.stringify(imgUploadDetailArray));
                renderToUploadedImgList(result.data.imagePath);
            }

        }).fail(function (error) {
            console.log(error);
        });
    };

    var renderToUploadedImgList = function (imagePath) {
        var source = $("#" + defaults.tmplNames.UploadedImgListItem).html();

        var template = Handlebars.compile(source);

        var renderedHtml = template({ imgSrc: imagePath });

        $(defaults.clsNames.UploadedImgList).append(renderedHtml);
    };

    var getImgUploadDetailArray = function ($el) {
        var tmpVal = $el.val();
        return tmpVal === "" ? [] : JSON.parse(tmpVal);
    };

    // Create Sayfasındaki Image 'ların Sil Button
    var initDeleteImageListItem = function () {
        $(defaults.clsNames.UploadedImgList).on('click', defaults.clsNames.PreviewListItemRemove, function () {
            var self = $(this);
            bootbox.setDefaults({ locale: 'tr' });
            bootbox.confirm("Delete image?", function (result) {
                if (result) {
                    var itemToDeleteKey = self.data('img-key');
                    self.closest('.preview-list-item').fadeOut(function () {
                        var $pageUploadedImgDetail = $(defaults.clsNames.PageUploadedImgDetail);
                        var imgUploadDetailArray = getImgUploadDetailArray($pageUploadedImgDetail);

                        for (var i = 0; i < imgUploadDetailArray.length; i++) {
                            var tmp = imgUploadDetailArray[i];
                            if (tmp['imagePath'] == itemToDeleteKey) {
                                imgUploadDetailArray.splice(i, 1);
                                $pageUploadedImgDetail.val(JSON.stringify(imgUploadDetailArray));
                                break;
                            }
                        }
                        $(this).remove();
                    });
                }
            });
        });
    };

    var initPageElements = function () {
        $(defaults.clsNames.ImgUploadModalOpener).click(function () {
            $(defaults.clsNames.ModalImgUpload).modal({ backdrop: true });
        });
    };

    initPageElements();
    initDeleteImageListItem();
    new ModalCropload().init({
        imgPickerEl: "imagePicker",
        modalAddImageCallback: modalAddImageCallback,
        imgUploadRoute: '/Upload/UploadNewscastImage'
    });
});