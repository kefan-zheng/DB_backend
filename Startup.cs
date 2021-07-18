using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LvDao.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LvDao.Tools;

namespace LvDao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //���cors ���� ���ÿ�����         
            services.AddCors(options =>
            {
                options.AddPolicy(name:"any", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//�Ƿ���ö�ǩ��securityToken��SecurityKey������֤
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenParameter.Secret)),//ǩ����Կ
                    ValidateIssuer = true,//�Ƿ���֤�䷢��
                    ValidIssuer = TokenParameter.Issuer, //�䷢��
                    ValidateAudience = true, //�Ƿ���֤������
                    ValidAudience = TokenParameter.Audience,//������
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LvDao's API Documents");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("any");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });

        }
    }
}
