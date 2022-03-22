using BPCalcAPI.DTOs.Request;
using BPCalcAPI.Mapper.Interfaces;
using BPCalcAPI.Workflow.Interfaces.Request;
using System;
using System.Collections.Generic;

namespace BPCalcAPI.Mappers
{
    public class ExternalRequestToWorkFlowMapper : IExternalRequestToWorkFlowMapper
    {
        public CalculateBenefitsCostWFRequest MapFrom(CalcRequestDto srcData)
        {

            if (srcData is null) throw new ArgumentNullException(nameof(srcData));

            CalculateBenefitsCostWFRequest retVal = new CalculateBenefitsCostWFRequest();
            var destMemberList = retVal.EmployeeAndFamilyList;

            if (destMemberList is null)
                destMemberList = new List<List<Entities.MemberInfo>>();


            //TODO: I can use AutoMapper here; 
            foreach (var memberAndDependents in srcData.Employees)
            {
                List<Entities.MemberInfo> familyInst = new List<Entities.MemberInfo>();

                foreach (var personInst in memberAndDependents)
                {
                    Entities.MemberInfo targetmemberAndDependents = new Entities.MemberInfo();
                    targetmemberAndDependents.MemberIdentifier = personInst.MemberIdentifier;
                    targetmemberAndDependents.FullName = personInst.FullName;
                    targetmemberAndDependents.MemberTypeCode = personInst.MemberTypeCode;

                    familyInst.Add(targetmemberAndDependents);
                }
                destMemberList.Add(familyInst);
            }

            return retVal;
        }
    }
}
