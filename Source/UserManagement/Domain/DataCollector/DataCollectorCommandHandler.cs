/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017 International Federation of Red Cross. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using doLittle.Domain;
using Domain.DataCollector.Registering;
using Domain.DataCollector.PhoneNumber;

namespace Domain.DataCollector
{
    public class DataCollectorCommandHandler : IDataCollectorCommandHandler
    {
        private readonly IAggregateRootRepositoryFor<DataCollector> _repository;

        public DataCollectorCommandHandler(
            IAggregateRootRepositoryFor<DataCollector> repository
            )
        {
            _repository = repository;
        }

        public void Handle(RegisterDataCollector command)
        {
            var root = _repository.Get(command.DataCollectorId);
            root.RegisterDataCollector(
                command.IsNewRegistration,
                command.FullName,
                command.DisplayName,
                command.YearOfBirth,
                command.Sex,
                command.NationalSociety,
                command.PreferredLanguage,
                command.GpsLocation,
                command.PhoneNumbers,
                command.RegisteredAt
                );
        }

        public void Handle(DeleteDataCollector command)
        {
            var root = _repository.Get(command.DataCollectorId);
            root.DeleteDataCollector(command.DataCollectorId);
        }


        public void Handle(AddPhoneNumberToDataCollector command)
        {
            var root = _repository.Get(command.DataCollectorId);
            root.AddPhoneNumber(command.PhoneNumber);
        }

        public void Handle(RemovePhoneNumberFromDataCollector command)
        {
            var root = _repository.Get(command.DataCollectorId);
            root.RemovePhoneNumber(command.PhoneNumber);
        }
    }
}
