var AjaxObj = function () {
    this.SuccessHandleFunction = null;
    this.Action = function (parameters, ajaxUrl, contentType, method) {
        $.ajax({
            type: method,
            url: ajaxUrl,
            async: false,
            contentType: contentType,
            data: parameters,
            success: function (e) {
                if (AjaxInstance.SuccessHandleFunction != null) {
                    AjaxInstance.SuccessHandleFunction(e.Data);
                }
            },
            error: function (e) {
                alert('Lỗi không thể kết nối đến server.');
            }
        });

    };
};
var AjaxInstance = new AjaxObj();

var HanghoaViewModel = function () {
    var self = this;
    // Data
    self.List = ko.observableArray([]);
    self.Current = ko.observable(null);
    self.SearchName = ko.observable("");

    // Danh sách nhóm hàng hóa để đổ ra combobox
    self.ListNhomHanghoa = ko.observableArray([]);

    //function
    self.CountSearch = ko.computed(function () {
        var i = 0;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.IsVisible() == true)
                i++;
        });
        return i;
    });


    self.InitData = function () {
        self.InitList();
    };
    self.InitList = function () {
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data != null && data.length > 0) {
                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) {
                    index++;
                    return new Hanghoa(item.HanghoaId, item.Code, item.Ten, item.Giaban, item.NgayGiaban, item.NhomHanghoaId, item.Dangban, null);
                });
                self.List.push.apply(self.List, listItems);
                if (self.List().length > 0)
                    self.Current(self.List()[0]);
            }
            // Bổ xung Tên nhóm hàng hóa vào object

        };

        AjaxInstance.Action("", "http://localhost:56173/Hanghoa/GetHanghoaItems", 'application/json; charset=utf-8', 'get');

        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data != null && data.length > 0) {
                $.each(data, function (key, value) {
                    self.ListNhomHanghoa.push(value);
                });
            }

            ko.utils.arrayForEach(self.List(), function (val1) {
                ko.utils.arrayForEach(self.ListNhomHanghoa(), function (val2) {
                    if(val1.NhomHanghoaId() == val2.NhomHanghoaId)
                    {
                        val1.TenNhomHanghoa(val2.Ten);
                    }
                });

            });
        };
        AjaxInstance.Action("", "http://localhost:56173/NhomHanghoa/GetNhomHanghoaItems", 'application/json; charset=utf-8', 'get');
    };

    //Action
    self.btnSetCurrent = function (item) {
        item.IsEdit(false);
        item.OldValue(ko.toJS(item));
        self.Current(item);
        
    };
    self.btnSearchName = function () {
        var iCurr = null;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.Ten().toLowerCase().indexOf(self.SearchName().toLowerCase()) != -1) {
                if (iCurr == null) {
                    iCurr = item;
                    self.Current(item);
                }
                item.IsVisible(true);
            }
            else {
                item.IsVisible(false);
            }
        });
    };
    self.CreateHanghoa = function () {
        var newItem = new Hanghoa(null, null, null, null, null, null, null);
        newItem.IsEdit(true);
        newItem.IsCreate(true);
        self.Current(newItem);
    };

    self.Thoat = function (item) {
        var obj = JSON.parse(ko.toJSON(item.OldValue())); // Lấy oldValue
        if (obj.HanghoaId) {// Nếu không có Id Nghĩa là người dùng ấn Edit rồi hũy   
            item.HanghoaId(obj.HanghoaId);
            item.Code(obj.Code);
            item.Ten(obj.Ten);
            item.Giaban(obj.Giaban);
            item.NgayGiaban(obj.NgayGiaban);
            item.NhomHanghoaId(obj.NhomHanghoaId);
            item.Dangban(obj.Dangban)
        }
        item.IsEdit(false);
        self.Current(self.List()[0]);
    };

    self.Edit = function (item) {
        item.IsEdit(true);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    }

    self.luuHanghoa = function (item) {
        if (!self.Current().isValid()) // Kiểm tra xem model này có đúng hay không
            return // Nếu sai thì return.
        

        item.NhomHanghoaId($("#NhomHanghoaId").val());
        var jsonData = ko.toJSON(self.Current());

        if (self.Current().IsCreate() == false) {
            AjaxInstance.SuccessHandleFunction = function (data) {
                item.TenNhomHanghoa($('#NhomHanghoaId option:selected').text());
                alert(data);
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/EditHanghoaItem", 'application/json; charset=utf-8', 'post');
        }
        else {
            AjaxInstance.SuccessHandleFunction = function (data) {
                var arr = data.split('-+-');

                if (arr[0] == "Thêm thành công") {
                    item.TenNhomHanghoa($('#NhomHanghoaId option:selected').text());
                    item.Dangban(false);
                    item.HanghoaId = arr[1];
                    self.List.push(item);
                }
                alert(arr[0]);
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/addHanghoaItem", 'application/json; charset=utf-8', 'post');
        }
        self.Current().IsCreate(false);
        self.Current().IsEdit(false);
    };
    self.remove = function (item) {
        if (confirm("Bạn có chắc chắn muốn xóa: " + item.Ten()) == false) {
            return;
        }
        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "Xóa thành công") self.List.remove(item);
            //alert(data);
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/deleteHanghoaItem", 'application/json; charset=utf-8', 'post');

        self.Current(self.List()[0]);
    }

    self.batdauBan = function (item) {
        if (confirm("Bạn có chắc chắn muốn bắt đầu bán món hàng: " + item.Ten() + " ?") == false) {
            return;
        }


        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                item.Dangban(true);
                alert("Món hàng " + item.Ten() + " đã được bắt đầu bán");
            }
            // alert(data);
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/batdauBan", 'application/json; charset=utf-8', 'post');
    }

    self.ngungBan = function (item) {
        if (confirm("Bạn có chắc chắn muốn ngưng bán món hàng: " + item.Ten() + "?") == false) {
            return;
        }

        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                item.Dangban(false);
                alert("Món hàng " + item.Ten() + " đã bị ngưng bán.");
            }
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/Hanghoa/ngungBan", 'application/json; charset=utf-8', 'post');
    }
};

$(document).ready(function () {
    var model = new HanghoaViewModel();
    model.InitData();

    ko.validation.init({
        decorateElement: true,
        errorMessageClass: 'text-error',
        insertMessages: true,
    });
    ko.applyBindings(model);
});

