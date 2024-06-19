namespace API_Corresponsales
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configuraciones de middleware, manejo de excepciones, etc.

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Mapeo de los controladores
        });
    }

}
