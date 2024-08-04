$(
    function () {
        var l = abp.localization.getResource('ProductManagement');
        var editModal = new abp.ModalManager(abp.appPath + 'Products/EditProductModal');


        var dataTable = $('#ProductsTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[0, "asc"]],
                searching: false,
                scrollX: true,
                ajax: abp.libs.datatables.createAjax(
                    productManagement.products.product.getList),
                columnDefs: [
                    {
                        title: l('Name'),
                        data: "name"
                    },
                    {
                        title: l('CategoryName'),
                        data: "categoryName",
                        orderable: false
                    },
                    {
                        title: l('Price'),
                        data: "price"
                    },
                    {
                        title: l('StockState'),
                        data: "stockState",
                        render: function (data) {
                            return l('Enum:StockState:' + data);
                        }
                    },
                    {
                        title: l('CreationTime'),             
                        data: "creationTime",
                        dataFormat: 'date'
                    },
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        action: function (data) {
                                            editModal.open({ id: data.record.id });
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        confirmMessage: function (data) {
                                            return l('ProductDeletionConfirmationMessage', data.record.name);
                                        },
                                        action: function (data) {
                                            productManagement.products.product.delete(data.record.id).then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                refreshTable();
                                            })
                                        }
                                    },
                                ]
                        }
                    },
                ]
            })
        );


        var createModal = new abp.ModalManager(abp.appPath + 'Products/CreateProductModal');
        createModal.onResult(function () {
            refreshTable();
        });
        $('#NewProductButton').click(function (e) {
            e.preventDefault();
            createModal.open();
        });

        editModal.onResult(function () {
            refreshTable();
        });
        function refreshTable() {
            dataTable.ajax.reload();
        }
    }
)();

