using BPCalcAPI.DTOs;
using BPCalcAPI.DTOs.Request;
using BPCalcAPI.Mappers;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace BPCalcTests
{
    [TestFixture]
    public class ExternalRequestToWorkFlowMapperTests
    {
        [Test]
        public void Test_Successfull_Mapping_Of_RequestData_To_WorkFlow_Object()
        {

            //Arrange
            CalcRequestDto inputData = new CalcRequestDto();
            inputData.Employees = new List<List<MemberDto>>();
            inputData.EffectiveDateToCalculateFor = "1/1/2022";

            for (int i = 0; i < 2; i++)
            {
                var familyList = new List<MemberDto>();

                var newMember = new MemberDto();
                newMember.FullName = "Sam Radley" + (i + 1).ToString();
                newMember.MemberTypeCode = "E";
                newMember.MemberIdentifier = "A" + (i + 1).ToString();

                familyList.Add(newMember);

                var newDependent = new MemberDto();
                newDependent.FullName = "Sue Radley" + (i + 1).ToString();
                newDependent.MemberTypeCode = "DS";
                newDependent.MemberIdentifier = "A" + (i + 1).ToString() + "_1";

                familyList.Add(newDependent);

                inputData.Employees.Add(familyList);
            }



            //Act
            var systemUnderTest = new ExternalRequestToWorkFlowMapper();
            var workflowReqInst = systemUnderTest.MapFrom(inputData);

            //Assert




            Assert.Multiple(() =>
            {
                inputData.Employees.Count.ShouldBeEquivalentTo(workflowReqInst.EmployeeAndFamilyList.Count);

                foreach (var employeeInst in inputData.Employees)
                {
                    foreach (var memberIns in employeeInst)
                    {
                        //find the same memberID in the destination collection
                        var memberFromDestinationObject = workflowReqInst.EmployeeAndFamilyList.
                                                            FirstOrDefault(x => x.Any(y => y.MemberIdentifier == memberIns.MemberIdentifier))
                                                            .FirstOrDefault(m => m.MemberIdentifier == memberIns.MemberIdentifier);

                        memberFromDestinationObject.ShouldNotBeNull();

                        memberFromDestinationObject.FullName.ShouldBeEquivalentTo(memberIns.FullName);
                        memberFromDestinationObject.MemberIdentifier.ShouldBeEquivalentTo(memberIns.MemberIdentifier);
                        memberFromDestinationObject.MemberTypeCode.ShouldBeEquivalentTo(memberIns.MemberTypeCode);
                    }
                }

            });

        }

    }
}
