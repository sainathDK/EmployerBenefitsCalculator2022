(
    function (bpcalculator, $) {

        bpcalculator.LastEmployerMemberId = 0;
        bpcalculator.LastDependantMemberId = 0;

        bpcalculator.LastAddedRowType = ""; //E (for employee row) or D (for dependant row)

        bpcalculator.LastUsedEmpUnqId = "";

        bpcalculator.EmployeeList = [];
        bpcalculator.EmployeeDependantMap = new Map();

        bpcalculator.ValidatorRef = null; //$("#dummyForm").validate();

        bpcalculator.FormatCurrency = function (number)
        {
            return number.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
        }

        bpcalculator.DisplayCalculationResults = function (calcResults)
        {
            //console.log("calcResults.length:" + calcResults.length);

            if (calcResults === undefined || calcResults.length === 0)
            {
                //console.log("There was no response to process");
                return;
            }
                

            for (let employeeCounter = 0; employeeCounter < calcResults.length; employeeCounter++)
            {
                let memberArray = calcResults[employeeCounter];

                for (let personCntr = 0; personCntr < memberArray.length; personCntr++)
                {
                    let memberIdentifier = memberArray[personCntr].memberIdentifier;
                    let membersBenefitCost = memberArray[personCntr].totalCostOfBenefitForTheMember;

                    let formattedCurrency = bpcalculator.FormatCurrency(membersBenefitCost);

                    if (memberIdentifier.includes("_"))
                    {
                        //for dependent child, spouse etc
                        $("#lblDep" + memberIdentifier).text(formattedCurrency);
                    }
                    else
                    {
                        //for parent i.e. employee
                        $("#lblEmp" + memberIdentifier).text(formattedCurrency);
                    }
                }
            }

        }

        bpcalculator.Initialize = function ()
        {
            //https://stackoverflow.com/a/65069937
            (async () => {
                //console.log("waiting for jQuery");

                while (!window.hasOwnProperty("jQuery")) {
                    await new Promise(resolve => setTimeout(resolve, 100));
                }

                //console.log("jQuery is loaded.");

                

                $("#btnAddEmployee").click(function ()
                {
                    //console.log("1");

                    if (bpcalculator.LastEmployerMemberId === 0)
                        bpcalculator.LastEmployerMemberId = 1;
                    else
                        bpcalculator.LastEmployerMemberId = bpcalculator.LastEmployerMemberId + 1;

                    let empUniqID = "A" + bpcalculator.LastEmployerMemberId;

                    //template literal - ES6
                    var htmlForNewTableForEmployee = `<table class='table table-condensed' id='tbl${empUniqID}'>
                        <thead class='EmpheaderRow'>
                        <tr>
                            <th class="firstCol">Employee</th>
                            <th>Complete Name or First Name</th>
                            <th>Member Type</th>
                            <th>Cost Of Benefits - Annually</th>
                        </tr>
                        </thead >
                        <tbody class='EmpbodyRow'>
                            <tr>
                                <td class="firstCol"></td>
                                <td><input data-unqId='${empUniqID}' id='txtEmpName${empUniqID}' style="width:80%;" type='text' placeholder="Employee's first name" /></td>
                                <td>Employee</td>
                                <td><label data-unqId='${empUniqID}' id='lblEmp${empUniqID}'>ToBeCalculated<label></td>
                            </tr>
                        </tbody>
                        </table>`;

                    if (bpcalculator.LastEmployerMemberId === 1)
                    {
                        $("#mainContainer").append(htmlForNewTableForEmployee);
                    }
                    else
                        $('table:last').after(htmlForNewTableForEmployee);

                    //console.log("empUniqID :" + empUniqID);
                    bpcalculator.LastAddedRowType = "E";
                    bpcalculator.LastUsedEmpUnqId = empUniqID;

                    bpcalculator.EmployeeList.push(empUniqID);
                });

                $("#btnAddDependent").click(function ()
                {

                    if (bpcalculator.LastUsedEmpUnqId === "") {
                        //console.log('can not add a dependant without a employee');
                        return;
                    }
                    //console.log("2");
                    if (bpcalculator.LastDependantMemberId === 0)
                        bpcalculator.LastDependantMemberId = 1;
                    else
                        bpcalculator.LastDependantMemberId = bpcalculator.LastDependantMemberId + 1;

                    let depUniqID = "A" + bpcalculator.LastEmployerMemberId + "_" + bpcalculator.LastDependantMemberId;

                    //console.log("depUniqID :" + depUniqID);

                    var htmlForNewHeaderAndRowForDependent =
                        `<thead id='tbody${depUniqID}'>
                        <tr>
                            <th class="firstCol">Dependent(s)</th>
                            <th>Complete Name or First Name</th>
                            <th>Member Type</th>
                            <th>Cost Of Benefits - Annually</th>
                        </tr>
                    </thead>
                    <tbody id='tbody${depUniqID}'>
                        <tr>
                            <td class="firstCol"></td>
                            <td><input data-unqId='${depUniqID}' id='txtDepName${depUniqID}' type='text' style="width:80%;" placeholder="Dependent's first name" /></td>
                            <td>
                                <select  id="sel${depUniqID}" data-val-required="Please select a Member Type" data-val="true">
                                <option value=""></option>
                                <option value="DS">Spouse</option>
                                <option value="DC">Child</option>
                                </select>
                            </td>
                            <td><label data-unqId='${depUniqID}' id='lblDep${depUniqID}'>ToBeCalculated<label></td>
                        </tr>
                    </tbody>`;

                    var htmlForNewRowOnlyForDependent =
                        `<tr id='tr${depUniqID}'>
                        <td class="firstCol"></td>
                        <td><input data-unqId='${depUniqID}' id='txtDepName${depUniqID}' type='text' style="width:80%;" placeholder="Dependent's first name" /></td>
                        <td>
                            <select id="sel${depUniqID}">
                            <option value=""></option>
                            <option value="DS">Spouse</option>
                            <option value="DC">Child</option>
                            </select>
                        </td>
                        <td><label data-unqId='${depUniqID}' id='lblDep${depUniqID}'>ToBeCalculated<label></td>
                    </tr>`;

                    if (bpcalculator.LastAddedRowType === "E")
                    {
                        $('tbody:last').after(htmlForNewHeaderAndRowForDependent);
                    }

                    if (bpcalculator.LastAddedRowType === "D")
                    {
                        $('tr:last').after(htmlForNewRowOnlyForDependent);
                    }

                    bpcalculator.LastAddedRowType = "D";

                    //check if there is an entry in the map for the employee identifier
                    let tempFromMap = bpcalculator.EmployeeDependantMap.get("A" + bpcalculator.LastEmployerMemberId);
                    if (tempFromMap === null || tempFromMap === undefined)
                    {
                        let tempArray = [];
                        tempArray.push(depUniqID)
                        bpcalculator.EmployeeDependantMap.set("A" + bpcalculator.LastEmployerMemberId, tempArray)
                    }
                    else
                    {
                        tempFromMap.push(depUniqID);
                        bpcalculator.EmployeeDependantMap.set("A" + bpcalculator.LastEmployerMemberId, tempFromMap)
                    }

                });

                //Not using this; there are some bugs;
                $("#btnDelete").click(function ()
                {
                    $('input:checked').each(function ()
                    {
                        //$(this).attr('name')
                        //console.log($(this));
                        //console.log($(this).attr("id"));
                        let unqId = $(this).attr("data-unqId");
                        let className = $(this).attr("class");

                        //console.log("className :" + className);

                        if (className.includes("DepChkBox"))
                        {
                            //trA12
                            $("#tr" + unqId).remove();
                        }

                        if (className.includes("PersonChkBox")) {
                            //tblA1
                            $("#tbl" + unqId).remove();
                        }

                    });

                });

                $("#btnResetPage").click(function ()
                {
                    //console.log("btnResetPage called");
                    location.reload();
                });


                $("#btnCompute").click(function () {

                    //console.log("compute called");

                    //Array
                    //bpcalculator.EmployeeList

                    //Map
                    //bpcalculator.EmployeeDependantMap

                    //dummyForm
                    if (bpcalculator.ValidatorRef===null)
                        bpcalculator.ValidatorRef = $("#dummyForm").validate();

                    let bIsValid = bpcalculator.ValidatorRef.form();

                    if (!bIsValid)
                        return;

                    let payLoadToSend = new Object();

                    payLoadToSend.EffectiveDateToCalculateFor = "01/01/2022";
                    payLoadToSend.Employees = [];

                    let familyArray = [];
                    let dataAvailableToSend = false;

                    for (let loopCntr = 0; loopCntr < bpcalculator.EmployeeList.length; loopCntr++)
                    {
                        dataAvailableToSend = true;
                        let memberInstance = new Object();
                        memberInstance.MemberIdentifier = bpcalculator.EmployeeList[loopCntr];
                        //get the employer first name / complete name
                        memberInstance.FullName = $("#txtEmpName" + bpcalculator.EmployeeList[loopCntr]).val();
                        memberInstance.MemberTypeCode = "E";
                        familyArray.push(memberInstance);

                        //now check the map for dependents added(if any) under the current employee
                        let dependentsArrayFromMap = bpcalculator.EmployeeDependantMap.get(bpcalculator.EmployeeList[loopCntr]);
                        if (dependentsArrayFromMap !== undefined)
                        {
                            for (let depLoopCntr = 0; depLoopCntr < dependentsArrayFromMap.length; depLoopCntr++) {
                                let dependantInstance = new Object();
                                dependantInstance.MemberIdentifier = dependentsArrayFromMap[depLoopCntr];
                                dependantInstance.FullName = $("#txtDepName" + dependentsArrayFromMap[depLoopCntr]).val();
                                //selA1_1
                                dependantInstance.MemberTypeCode = $('#sel' + dependentsArrayFromMap[depLoopCntr]).val();
                                familyArray.push(dependantInstance);
                            }
                        }
                        payLoadToSend.Employees.push(familyArray);

                        //reinitialize the array
                        familyArray = [];
                    }

                    let jsonPayLoadToSend = JSON.stringify(payLoadToSend);

                    let urlForSvc = $("#txtUrlForCalcSvc").val() + "";

                    if (urlForSvc !== "" && dataAvailableToSend===true)
                    {
                        //console.log(urlForSvc);
                        //console.log(jsonPayLoadToSend);

                        var jqxhr = $.ajax({
                            url: urlForSvc,
                            type: 'POST',
                            data: jsonPayLoadToSend,
                            success: function (responseData)
                            {
                                //console.log("success");
                                //console.log("responseData :" + JSON.stringify(responseData));
                                bpcalculator.DisplayCalculationResults(responseData.memberCostDetails);

                                $("#postProcessingConfirmation").modal("show");

                            },
                            contentType: "application/json;charset=utf-8"
                        })
                            .done(function () {
                                //console.log("second success");
                            })
                            .fail(function ()
                            {
                                //alert("Something went wrong while processing the calculations. Please try again. If error persists, contact helpdesk at 999-999-9999");
                                //errorModalPopUp
                                $('#errorModalPopUp').modal("show");
                            })
                            .always(function () {
                                //console.log("always - completed");
                            });

                        // Perform other work here ...

                        // Set another completion function for the request above
                        jqxhr.always(function () {
                            //console.log("second finished");
                        });


                    }
                });

            })();
        };


    }(window.bpcalculator = window.bpcalculator || {}, jQuery)
);

bpcalculator.Initialize();