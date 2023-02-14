# CSPharma-v4.3.0

- [CSPharma-v4.3.0](#cspharma-v430)
  - [Introducción](#introducción)
- [1. Live Search Bar con Jquery](#1-live-search-bar-con-jquery)
- [2. Autoasignar rol al registrar nuevo usuario](#2-autoasignar-rol-al-registrar-nuevo-usuario)

## Introducción

En esta continuación de la v4.2.2, vamos a añadir una pantalla de administración de usuarios (CRUD) con acciones de asignación de rol y eliminar usuario.

Por otro lado, añadiremos la funcionalidad de que cuando un nuevo usuario se registre, se le autoasignará el rol básico de Users.

Finalmente, también se implementará una barra de búsqueda en directo sobre los demás CRUDs de las entidades de la primera versión.

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

# 2. Autoasignar rol al registrar nuevo usuario

Para conseguir esta parte, he tenido que deshacer algunos cambios del sprint anterior...

Haciendo un poco de memoria, en el sprint 2, el profesor nos pidió que creásemos tres roles en la BBDD.

ID --> ROL
- 0 --> Administrators
- 1 --> Employees
- 2 --> Users

Entonces yo los introduje en la BBDD manualmente.

Después registré a tres usuarios.

Y finalmente fui a la tabla relacional de identity de Dlk_cat_acc_empleados_roles (AspNetUsersRoles) a relacionar manualmente los tres roles con los tres usuarios.

- 0 --> Administrators --> sergio
- 1 --> Employees --> moises
- 2 --> Users --> javier

El problema que me he encontrado de primeras a la hora de hacer este nuevo punto, es que si un desarrollador introduce manualmente los roles en la BBDD, después la clase RoleManager no es capaz de identificarlos.

Es decir, RoleManager necesita crear él mismo los roles para poder después manejarlos.

Entonces, para respetar los roles que tenía definidos en mis tres usuarios, y dejar esta parte lista para que los nuevos usuarios que se registren se les autoasigne el rol de Users, he hecho el siguiente truco...

He eliminado en la BBDD los tres usuarios que tenía y los tres roles que introduje manualmente.

Después, en el código del método OnPostAsync() del Register.cshtml.cs he añadido la parte nueva de gestión de roles con RoleManager, para que en la siguiente ejecucuión de mi proyecto, RoleManager cree el rol de Administrators, y el siguiente usuario que se registre (sergio) se le asigne el rol de administrador.

Registro al usuario sergio y efectivamente se le asigna el rol Administrators.

Cierro la ejecución del proyecto y voy de nuevo al método OnPostAsync() del Register.cshtmml.cs para que ahora cree el rol de Employees, y ejecuto otra vez el proyecto y registro al usuario moises.

Finalmente cierro de nuevo el proyecto, para ahora en el Register dejar definido definitivamente que todo usuario nuevo que se registre se le asigne el rol Users.

Ejecuto una tercera vez el proyecto y ahora registro al usuario de javier.

Y ya estaría hecho este punto. A partir de ahora todo nuevo usuario que se registre se el autoasignará el rol básico de Users.

```csharp
public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    /******************************** to assign default role ************************************/
                    // forma 1 --> no funciona: El rol USERS no se encuentra en la bbdd
                    /*
                    var defaultRole = _roleManager.FindByIdAsync("2").Result;

                    if (defaultRole != null)
                    {
                        await _userManager.AddToRoleAsync(user, defaultRole.Name);
                    }
                    */

                    // forma 1 --> no funciona: encuentra el rol pero no lo asigna al usuario
                    /*
                    var roleExist = await _roleManager.RoleExistsAsync("Users");
                    
                    if (roleExist)
                    {
                        await _userManager.AddToRoleAsync(user, "Users");
                    }
                    */

                    // forma 3 --> SÍ funciona: primero creamos el rol y después lo asignamos
                    // explicación: para que RoleManager identifique los roles de la bbdd,
                    // es necesario que sea él mismo quien los cree, porque si los creo yo manualmente, ya no se reconocen
                    // https://stackoverflow.com/questions/70559504/invalidoperationexception-role-admin-does-not-exist
                    var roleExist = await _roleManager.RoleExistsAsync("Users");
                    
                    if (!roleExist)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Users"));
                    }

                    await _userManager.AddToRoleAsync(user, "Users");

                    /*********************************************************************************************/
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
```

![](./img/1.png)

![](./img/2.png)

![](./img/3.png)

[Prueba de Ejecución 2 - Autoasignar rol al registrar nuevo usuario](https://user-images.githubusercontent.com/91122596/218782404-f590b032-db84-4b18-9fb9-2d9770e2893d.mp4)