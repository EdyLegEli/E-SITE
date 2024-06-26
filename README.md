# E-SITE
# LMS-MOODLE
CREACIÓN DE PLATAFORMA DE APRENDIZAJE 
## LINK DE DESCARGA MOODLE VERSION 4.1.3
https://download.moodle.org/windows/

***Los archivos zips estaran guardados en este enlace del drive, ya que por tamaño no se puede añadir al git  https://drive.google.com/drive/folders/1Gw_mWMYR42d_SMIfxXS2R4Psp8aIx1DR?usp=sharing "

-Instaladores:

-Una vez instalado el servidor Apache (ejem: XAMPP), se debe actualizar la version de MariaDb de la version 10.4 a la version 10.6.7 con los archivos dentro de la carpeta de instaladores como lo dice el siguiente tutorial:

https://www.youtube.com/watch?v=VbEMM5L4m9U

---------------------------------------------------

-Después de ello para la instalación del sitio moodle se ingresa la información de la carpeta para la data del sitio que se encuentra en la carpeta moodle data y después de ello se da ingresa la información de las credenciales para la conxión con la base de datos a utilizar.

-Para solucionar los problemas de configuración propuestos durante la instalación se debe realizar unos cambios a nivel del archivo php.ini dentro del servidor donde se debe habilitar las sguientes extensiones:

extension=gd
extension=intl
extension=sodium
extension=zip
max_input_vars = 5000

Todo estas configuraciones estan igualmente explicado en el tutorial anterior listado anteriormente:

https://www.youtube.com/watch?v=VbEMM5L4m9U


Archivos de configuracion:

-Para la instalación del sitio se debe poner el contenido de la carpeta e-site dentro de htdocs dentro del servidor (ejm: XAMPP).
-También dentro del servidor (ejem: XAMPP) se debe colocar la carpeta moodledata.

------------------------------------------------------------------------------------

Nuevos archivos:

En el proyecto, dentro de la carpeta prueba_e-site/block se coloca esta nueva carpeta de testblock que es un nuevo componente de la plataforma.

--------------CONOCIMIENTOS BASICOS-------------------------------------

PHP  https://www.php.net/manual/en/tutorial.php
moodle https://docs.moodle.org/dev/Tutorial#The_skeleton_of_your_plugin
https://docs.moodle.org/dev/Creating_a_theme_based_on_boost

Enjoy coding!

-------------------------------------------------------------------------

Nuevos archivos 25/06/2024

Cambios_realizados_en_theme_photos