﻿using BackendApp.Controllers;

namespace BackendApp.Services
{
    public class PeopleService: IPeopleService
    {
        public bool Validate(People people) {
            if (string.IsNullOrEmpty(people.Name))
            {
                return false;
            }

            return true;
        }
    }
}
