Instalaciones en vue:

npm install vee-validate --save 
npm install @vee-validate/yup 

npm install axios 

npm install date-fns 

npm install @fortawesome/fontawesome-free

IMPORTANTE

En el back-end, en Program.cs, Modificar el Origen permitido

builder.Services.AddCors(options => //  --  POLITICA TEMPORAL  --
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

