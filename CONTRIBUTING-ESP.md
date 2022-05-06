<!-- omit in toc -->
# Contribuyendo a ScioXR

Primero que nada, ¡Gracias por tomarse el tiempo para contribuir! ❤️

Todos los tipos de contribuciones son alentados y valorados. Vea la [Tabla de Contenidos](#tabla-de-contentido) para conocer las diferentes formas de ayudar y los detalles sobre cómo este proyecto les maneja. Por favor, asegúrese de leer la sección correspondiente antes de hacer su contribución. Hará que sea mucho más fácil para nosotros los mantenedores y suavizará la experiencia para todos los involucrados. La comunidad espera sus contribuciones. 🎉

> Y si le gusta el proyecto, pero justo no tiene tiempo para contribuir, está bien. Hay otras formas sencillas de apoyar el proyecto y mostrar su aprecio, lo cual nos haría también muy felices. 
> - Protagoniza el proyecto
> - Twittea acerca de eso
> - Refiera este proyecto en el archivo Léame de su proyecto
> - Mencione el proyecto en reuniones locales y cuénteselo a sus amigos/colegas
<!-- omit in toc -->
## Tabla de Contenidos

- [Código de Conducta](#código-de-conducta)
- [Tengo una Pregunta](#tengo-una-pregunta)
- [Quiero Contribuir](#quiero-contribuir)
  - [Reportando Errores](#reportando-errores)
  - [Sugiriendo Mejoras](#sugiriendo-mejoras)
  - [Su Primera Contribución de Código](#su-primera-contribución-de-código)
  - [Mejorando la Documentación](#mejorando-la-documentación)
- [Guías de Estilo](#guías-de-estilo)
  - [Confirmar Mensajes](#confirmar-mensajes)
- [Únase al Equipo del Proyecto](#únase-al-equipo-del-proyecto)


## Código de Conducta

Este proyecto y todos los que están participando en este se rigen por el 
[Código de Conducta de ScioXR](https://github.com/propter-rs/scioxr-dev/master/CODE_OF_CONDUCT.md).
Al estar participando, se espera que respete este código. Por favor reporte cualquier comportamiento inaceptable a <contact@scioxr.com>.


## Tengo una Pregunta

> Si desea realizar una pregunta, asumimos que ha leído la disponible [Documentación](docs.scioxr.com).
Antes de realizar una pregunta, es mejor buscar [Problemas](https://github.com/propter-rs/scioxr-dev/issues) existentes que podrían ayudarlo. En caso de que haya encontrado un problema adecuado y aún necesite una aclaración, puede escribir su pregunta en este problema. También es aconsejable buscar primero en Internet las respuestas.


Si en este caso aún siente la necesidad de realizar una pregunta y necesita una aclaración, le recomendamos lo siguiente:


- Abra un [Problema](https://github.com/propter-rs/scioxr-dev/issues/new).
- Proporcione todo el contexto que pueda acerca de lo que está encontrando.
- Proporcione versiones de proyectos y plataformas (nodejs, npm, etc.), dependiendo de lo que parezca relevante.

En ese caso, nos ocuparemos del problema lo antes posible.

<!--
You might want to create a separate issue tag for questions and include it in this description. People should then tag their issues accordingly.
Depending on how large the project is, you may want to outsource the questioning, e.g. to Stack Overflow or Gitter. You may add additional contact and information possibilities:
- IRC
- Slack
- Gitter
- Stack Overflow tag
- Blog
- FAQ
- Roadmap
- E-Mail List
- Forum
-->

## Quiero Contribuir

> ### Aviso Legal <!-- omit in toc -->
> Cuando se está contribuyendo a este proyecto, debe aceptar que ha sido el autor del 100 % del contenido, que tiene los derechos necesarios sobre el contenido y que el contenido con el  que contribuye puede ser proporcionado bajo la licencia del proyecto.
### Reportando Errores

<!-- omit in toc -->
#### Antes de Enviar un Reporte de Errores

Un buen reporte de errores no debería dejar a otros necesitando perseguirlo para obtener más información. Por lo tanto, le pedimos que investigue cuidadosamente, recolecte información y describa el problema en detalle en su reporte. Por favor complete los siguientes pasos con anticipación para ayudarnos a corregir cualquier posible error lo más rápido posible.

- Asegúrese de que está utilizando la última versión.
- Determine si su error es realmente un error y no un error de su lado, p. utilizando componentes/versiones de entorno incompatibles (asegúrese de haber leído la [documentación](docs.scioxr.com).  Si está buscando soporte, podría querer consultar [esta sección](#i-have-a-question)).
- Para ver si otros usuarios han experimentado (y potencialmente ya han solucionado) el mismo problema que usted está teniendo, verifique allí si aún no existe un reporte de error para su error o error en el [rastreador de errores](https://github.com/propter-rs/scioxr-devissues?q=label%3Abug).
- También asegúrese de buscar en Internet (incluyendo Stack Overflow) para ver si los usuarios fuera de la comunidad de GitHub han discutido el problema.
- Recolecte información sobre el error:
  - Rastreo de pila (Traceback)
  - OS, Plataforma y Versión (Windows, Linux, macOS, x86, ARM)
  - Versión del intérprete, compilador, SDK, entorno de tiempo de ejecución, gestor de paquetes, según lo que parezca relevante.
  - Posiblemente su entrada y la salida
  - ¿Puedes reproducir el problema de forma fiable? ¿Y también se puede reproducir con versiones anteriores?

<!-- omit in toc -->
#### ¿Cómo Envío un Buen Reporte de Error?

> Nunca debe reportar problemas relacionados con la seguridad, vulnerabilidades o errores al rastreador de problemas, o en cualquier otro lugar en público. En su lugar, los errores sensibles deben enviarse por correo electrónico a <contact@scioxr.com>.
<!-- You may add a PGP key to allow the messages to be sent encrypted as well. -->
Usamos problemas de GitHub para rastrear errores y fallas. Si se encuentra con un problema con el proyecto:

- Abra un [Problema](https://github.com/propter-rs/scioxr-devissues/new). (Dado que en este punto no podemos estar seguros de si se trata de un error o no, le pedimos que no hable sobre un error todavía y que no etiquete el problema).
- Explique el comportamiento que esperaría y el comportamiento actual.
- Por favor proporcione todo el contexto posible y describa los pasos de reproducción que otra persona puede seguir de otro modo para recrear el problema por su cuenta. Esto generalmente incluye su código. Para obtener buenos reportes de errores, debería aislar el problema y crear un caso de prueba reducido.
- Proporcione la información que recopiló en la sección previa.

Una vez presentado:

- El equipo del proyecto etiquetará el problema respectivamente.
- Un miembro del equipo intentará reproducir el problema con los pasos que fueron proporcionados. Si no hay pasos de reproducción o una forma obvia de reproducir el problema, el equipo le pedirá esos pasos y marcará el problema como `needs-repro`. Los errores con la etiqueta `needs-repro` no se abordarán hasta que se reproduzcan.
- Si el equipo puede reproducir el problema, se marcará como `needs-fix`, así como posiblemente con otras etiquetas (como `critical`), y el problema se dejará para que [alguien lo implemente.](#your-first-code-contribution).

<!-- You might want to create an issue template for bugs and errors that can be used as a guide and that defines the structure of the information to be included. If you do so, reference it here in the description. -->


### Sugiriendo Mejoras

Esta sección lo guía a través del envío de una sugerencia de mejora para ScioXR, **incluyendo características completamente nuevas y menores mejoras en las funcionalidades existentes**. Siguiendo estas pautas ayudará a los mantenedores y a la comunidad a comprender su sugerencia y encontrar sugerencias relacionadas.

<!-- omit in toc -->
#### Antes de Enviar una Mejora

- Asegúrese de que está utilizando la última versión.
- Lea la [documentación](docs.scioxr.com) detenidamente y descubra si la funcionalidad ya está cubierta, quizás por una configuración individual.
- Realice una [búsqueda](https://github.com/propter-rs/scioxr-dev/issues) para ver si ya ha sido sugerida la mejora. Si es así, agregue un comentario al problema existente en lugar de abrir uno nuevo.
- Averigüe si su idea encaja con el alcance y los objetivos del proyecto. Depende de usted presentar un caso sólido para convencer a los desarrolladores del proyecto de los méritos de esta característica. Tenga en cuenta que queremos funciones que sean útiles para la mayoría de nuestros usuarios y no solo para un pequeño subconjunto. Si solo se dirige a una minoría de usuarios, considere escribir una biblioteca de complementos/complementos.

<!-- omit in toc -->
#### ¿Cómo Envío una Buena Sugerencia de Mejora?

Las sugerencias de mejora se rastrean como [problemas de GitHub](https://github.com/propter-rs/scioxr-dev/issues).

- Use un **título claro y descriptivo** para el problema para identificar la sugerencia.
- Proporcione una **descripción paso por paso de la mejora sugerida** con tantos detalles como sea posible.
- **Describa el comportamiento actual** y **explique qué comportamiento esperaba ver en su lugar** y por qué. En este punto también puede saber cuáles alternativas no trabajan para usted.
- Puede que desee **incluir capturas de pantalla y GIFs animados** que lo ayuden a demostrar los pasos o señalar la parte con la cual se relaciona la sugerencia. Puede usar [esta herramienta](https://www.cockos.com/licecap/) para grabar GIFs en macOS y Windows, y [esta herramienta](https://github.com/colinkeenan/silentcast) o [esta herramienta](https://github.com/GNOME/byzanz) en Linux. <!-- this should only be included if the project has a GUI -->
- **Explique por qué esta mejora sería útil** para la mayoría de los usuarios de ScioXR. También puede querer señalar otros proyectos que lo resolvieron mejor y los cuales podrían servirle de inspiración.

<!-- You might want to create an issue template for enhancement suggestions that can be used as a guide and that defines the structure of the information to be included. If you do so, reference it here in the description. -->

### Su Primera Contribución de Código
<!-- TODO
include Setup of env, IDE and typical getting started instructions?
-->

### Mejorando la Documentación
<!-- TODO
Updating, improving and correcting the documentation
-->

## Guías de Estilo
### Confirmar Mensajes
<!-- TODO
-->

## Únase al Equipo del Proyecto
<!-- TODO -->

<!-- omit in toc -->
## Atribución
Esta guía se basa en la **contribución-gen**. ¡[Haz lo tuyo](https://github.com/bttger/contributing-gen)!