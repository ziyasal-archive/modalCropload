function ModalCropload() {
    var defaults = {
        allowResize: true,
        jCropSetSelect: [0, 0, 750, 500],
        minSize: [750, 200],
        maxSize: [750, 2000],
        minPreviewHeight: 200,
        previewElemMaxWidth: '750px',
        clsNames: {
            ModalAddButton: '.modal-add-button',
            ModalCancelButton: '.modal-cancel-button',
            ModalUploadedImgDetail: '.modal-uploaded-img-detail',
            ModalUploadedImgKey: '.modal-uploaded-img-key',
            ModalImgPreviewField: '.img-preview-field',
            ModalImgUpload: '.modal-img-upload'
        }
    };



    var jcropApi = null;

    var initModalEvents = function () {

        // BTN: Add : Click
        var $modalBtnAdd = $(defaults.clsNames.ModalAddButton);
        $modalBtnAdd.click(function () {

            var $modalUploadedImgDetail = $(defaults.clsNames.ModalUploadedImgDetail);
            var $modalUploadedImgKey = $(defaults.clsNames.ModalUploadedImgKey);

            var modalHiddenImageDetail = $modalUploadedImgDetail.val();
            if (modalHiddenImageDetail) {
                var tmpUploadedImgDetail = JSON.parse(modalHiddenImageDetail);

                var imgKey = $modalUploadedImgKey.val();
                tmpUploadedImgDetail['imgKey'] = imgKey;

                if (defaults.modalAddImageCallback && typeof defaults.modalAddImageCallback == "function")
                    defaults.modalAddImageCallback(tmpUploadedImgDetail);
            }

            $(defaults.clsNames.ModalImgUpload).modal('hide');
        });

        // BTN: Cancel : Click
        var $modalCancelBtn = $(defaults.clsNames.ModalCancelButton);
        $modalCancelBtn.click(function () {
            destroyJCrop();
            if (defaults.modalCancelCallback && typeof defaults.modalCancelCallback == "function")
                defaults.modalCancelCallback();

            $(defaults.clsNames.ModalImgUpload).modal('hide');
        });

        // ON : Modal : Shown
        $(defaults.clsNames.ModalImgUpload).on('hidden.bs.shown', function (e) {
            $(defaults.clsNames.ModalImgPreviewField).attr('src', '');
        });

        // ON : Modal : Hide
        $(defaults.clsNames.ModalImgUpload).on('hidden.bs.modal', function (e) {
            $(defaults.clsNames.ModalImgPreviewField).attr('src', '');

            destroyJCrop();
            clearModalHiddens();
        });

    };

    function initJCrop(responseJson) {
        $(defaults.clsNames.ModalUploadedImgKey).val(responseJson.tempImageKey);
        var tmpImageElement = $(defaults.clsNames.ModalImgPreviewField);

        if (defaults.minPreviewHeight !== null) {
            tmpImageElement.css('min-height', defaults.minPreviewHeight);
        }

        tmpImageElement.attr("src", responseJson.imageUrl);
        tmpImageElement.css({ 'max-width': defaults.previewElemMaxWidth });
        tmpImageElement.Jcrop({
            bgOpacity: 0.6,
            allowSelect: false,
            allowMove: true,
            allowResize: true,
            minSize: defaults.minSize,
            maxSize: defaults.maxSize,
            setSelect: defaults.jCropSetSelect,
            onChange: setJCropCoordinates,
            onSelect: setJCropCoordinates
        }, function () {
            jcropApi = this;
        });
    }

    // FileUpload Component (Modal 'daki Fotoğraf Seç Button)
    var initFileUpload = function () {
        var $imgPickerElement = $('#' + defaults.imgPickerEl);

        if (defaults.imgUploadRoute) {
            $imgPickerElement.attr('data-url', defaults.imgUploadRoute);
        }

        $imgPickerElement.click(function () {
            if (jcropApi) {
                jcropApi.destroy();
            }
        });

        $imgPickerElement.fileupload({
            dataType: 'json',
            done: function (e, data) {
                var warningEl = $(".upload-warnings");
                var responseJson = data.result;
                if (responseJson.success === false) {
                    warningEl.show();
                    warningEl.html(responseJson.message);
                } else {
                    if (warningEl.is(":visible")) {
                        warningEl.hide();
                    }
                    $(defaults.clsNames.ModalUploadedImgKey).val(responseJson.tempImageKey);
                    initJCrop(responseJson);
                }
            }
        });
    };

    // JCrop Selection Coordinate
    function setJCropCoordinates(coordinate) {
        $(defaults.clsNames.ModalUploadedImgDetail).val(JSON.stringify({
            'x': coordinate.x,
            'y': coordinate.y,
            'Width': coordinate.x2,
            'Height': coordinate.y2,
            'PreviewImageWidth': $(defaults.clsNames.ModalImgPreviewField).width(),
            'PreviewImageHeight': $(defaults.clsNames.ModalImgPreviewField).height()
        }));
    }

    var clearModalHiddens = function () {
        var $modalUploadedImgDetail = $(defaults.clsNames.ModalUploadedImgDetail);
        var $modalUploadedImgKey = $(defaults.clsNames.ModalUploadedImgKey);

        $modalUploadedImgDetail.val('');
        $modalUploadedImgKey.val('');
    };

    var destroyJCrop = function () {
        if (!jcropApi) {
            return;
        }

        jcropApi.destroy();
        var tmpImageElement = $(defaults.clsNames.ModalImgPreviewField);
        tmpImageElement.attr('src', '');
        tmpImageElement.removeAttr('style');
    };

    return {
        init: function (options) {
            defaults = $.extend(defaults, options, true);
            initModalEvents();
            initFileUpload();
        }
    }
};