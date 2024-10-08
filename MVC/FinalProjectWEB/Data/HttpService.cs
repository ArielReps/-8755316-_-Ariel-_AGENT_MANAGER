﻿using FinalProjectWEB.Models;
using FinalProjectWEB.Models.AuxiliaryModels;
using FinalProjectWEB.Models.BaseModels;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace FinalProjectWEB.Data
{
    public class HttpService : IHttpService
    {
        private readonly string _url = "http://localhost:5008/";
        private readonly HttpClient _client;
        public HttpService()
        {
            _client = new HttpClient();
            Login().Wait();
        }
        
        public async Task<bool> ConfirmMission(int aid, int tid, MissionStatus status)
        {
            try
            {
                var response = await _client.PutAsJsonAsync(_url + $"missions/{aid}/{tid}", status);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Agent>> GetAgentsAsync()
        {
            try
            {
                Console.WriteLine(_client.DefaultRequestHeaders.Authorization.Parameter);
                var response = await _client.GetAsync(_url + "agents");
                string json = await response.Content.ReadAsStringAsync();
                IEnumerable<Agent> agents = JsonConvert.DeserializeObject<IEnumerable<Agent>>(json)!;
                return agents;
            }catch (Exception ex)
            {
                throw new Exception("Can't connected");
            }
        }

        public async Task<IEnumerable<Mission>> GetMissionsAsync()
        {
            try
            {
                var response = await _client.GetAsync(_url + "missions");
                string json = await response.Content.ReadAsStringAsync();
                IEnumerable<Mission> missions = JsonConvert.DeserializeObject<IEnumerable<Mission>>(json)!;
                return missions;
            }
            catch (Exception ex)
            {
                throw new Exception("Can't connected");
            }
        }

        public async Task<IEnumerable<Mission>> GetMissionsOffersAsync()
        {
            try
            {
                var response = await _client.GetAsync(_url + "missions/offers");
                string json = await response.Content.ReadAsStringAsync();
                IEnumerable<Mission> missions = JsonConvert.DeserializeObject<IEnumerable<Mission>>(json)!;
                return missions;
            }
            catch (Exception ex)
            {
                throw new Exception("Can't connected");
            }
        }

        public async Task<IEnumerable<Target>> GetTargetsAsync()
        {
            try
            {
                var response = await _client.GetAsync(_url + "targets");
                string json = await response.Content.ReadAsStringAsync();
                IEnumerable<Target> targets = JsonConvert.DeserializeObject<IEnumerable<Target>>(json)!;
                return targets;
            }
            catch (Exception ex)
            {
                throw new Exception("Can't connected");
            }
        }

        public async Task Login()
        {
            try
            {
                var res = await _client.PostAsJsonAsync(_url + "login", new {id = "MVCServer" });
                string json = await res.Content.ReadAsStringAsync();
                LoginModel model = JsonConvert.DeserializeObject<LoginModel>(json)!;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.token);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't connected");
            }
        }

        public async Task<IEnumerable<EntityInSpace>> GetEntitiesAsync()
        {
            List<EntityInSpace> entities = new List<EntityInSpace>();
            try
            {
                var response = await _client.GetAsync(_url + "targets");
                string json = await response.Content.ReadAsStringAsync();
                IEnumerable<Target> targets = JsonConvert.DeserializeObject<List<Target>>(json)!;
                targets = targets.Where(t => t.Status != TargetStatus.Eliminated).ToList();

                response = await _client.GetAsync(_url + "agents");
                json = await response.Content.ReadAsStringAsync();
                IEnumerable<Agent> agents = JsonConvert.DeserializeObject<List<Agent>>(json)!;

                foreach (var item in targets)
                {
                    entities.Add(item);
                }
                foreach (var item in agents)
                {
                    entities.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can't connected");
            }
            return entities;
        }
    }
}
