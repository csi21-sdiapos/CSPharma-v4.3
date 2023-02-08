# CSPharma-v4.3.0

## Introducción

En esta continuación de la v4.2.2, vamos a añadir una pantalla de administración de usuarios (CRUD) con acciones de asignación de rol y eliminar usuario.

Por otro lado, también se implementará una barra de búsqueda en directo sobre los demás CRUDs de las entidades de la primera versión.

# 1. Live Search Bar con Jquery

Empezamos por aquí porque es lo más sencillo y rápido de implementar.

Tan sólo tenemos que ir a alguno de los Index de nuestros CRUDs y añadir un input identificado por el campo que queramos buscar: <input type="text" id="searchId" placeholder="buscar por ID" />


```html
<input type="text" id="searchId" placeholder="buscar por ID" />
```

Abajo de toda la página, importamos la librería de Jquery (ya sea a través de la librería estática por defecto que nos trae el proyecto, o buscando nosotros mismos el CDN de Jquery).

```html
<script src="~/lib/jquery/dist/jquery-3.6.3.min.js" asp-append-version="true"></script>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>
```

Finalmente, añadimos el siguiente `<script>` debajo de la importación de Jquery:

```js
<script>
    $("#searchId").on("keyup", function () {
        var inputSearch = $(this).val();

        $("table tr").each(function (results) {
            if (results !== 0) {
                var search = $(this).find("td:nth-child(3)").text(); // nth-child(3) --> Id

                if (search.indexOf(inputSearch) !== 0 && search.toLowerCase().indexOf(inputSearch.toLowerCase()) < 0) {
                    $(this).hide();
                }
                else {
                    $(this).show();
                }
            }
        });
    });
</script>
```

Para filtrar por cualquier otro campo, tan sólo tendríamos que replicar este script cambiando el identificador por id del input, y la posición que ocupa el campo por el cual queremos filtrar.

[Prueba de Ejecución 1 - Live Search Bar](https://user-images.githubusercontent.com/91122596/217490957-fd701d87-2f46-4c7e-a13f-67e40c31d677.mp4)