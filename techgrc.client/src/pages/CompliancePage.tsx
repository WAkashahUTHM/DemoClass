import React, { useEffect, useState } from "react";
import axios from "axios";
import { Grid, Paper, Typography, Button, CircularProgress } from "@mui/material";
import { ProductionProcessComplianceDTO } from "../models/complianceModels";  // Stelle sicher, dass du das DTO korrekt verwendest.

const CompliancePage: React.FC = () => {
    const [complianceData, setComplianceData] = useState<ProductionProcessComplianceDTO | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    const productionProcessId = "some-production-process-id"; // Ersetze dies mit der tatsächlichen Produktionsprozess-ID

    useEffect(() => {
        const fetchComplianceData = async () => {
            try {
                const response = await axios.get(`/api/compliance/process/${productionProcessId}/compliance`);
                setComplianceData(response.data);
                setLoading(false);
            } catch (err) {
                setError("An error occurred while fetching compliance data.");
                setLoading(false);
            }
        };

        fetchComplianceData();
    }, [productionProcessId]);

    if (loading) {
        return <CircularProgress />;
    }

    if (error) {
        return <Typography color="error">{error}</Typography>;
    }

    return (
        <Grid container spacing={3}>
            <Grid item xs={12}>
                <Typography variant="h4" gutterBottom>
                    Compliance Monitoring for {complianceData?.ProcessName}
                </Typography>
            </Grid>

            <Grid item xs={12}>
                <Paper elevation={3} sx={{ padding: 2 }}>
                    <Typography variant="h6">Compliance Status</Typography>
                    <Typography variant="body1">
                        {complianceData?.LinkedComplianceStandards.length > 0
                            ? "Compliance standards for this process are listed below."
                            : "No compliance standards found for this process."}
                    </Typography>

                    {complianceData?.LinkedComplianceStandards.map((standard) => (
                        <Grid key={standard.ComplianceStandardId} container spacing={2}>
                            <Grid item xs={6}>
                                <Typography variant="body1">{standard.StandardName}</Typography>
                                <Typography variant="body2">{standard.Description}</Typography>
                            </Grid>
                            <Grid item xs={6} textAlign="right">
                                <Typography variant="body2" color={standard.IsCompliant ? "green" : "red"}>
                                    {standard.IsCompliant ? "Compliant" : "Non-Compliant"}
                                </Typography>
                                <Typography variant="body2">Last Checked: {standard.LastChecked}</Typography>
                            </Grid>
                        </Grid>
                    ))}
                </Paper>
            </Grid>

            <Grid item xs={12} mt={2}>
                <Button variant="contained" color="primary" onClick={() => alert('Trigger compliance update logic')}>
                    Update Compliance Status
                </Button>
            </Grid>
        </Grid>
    );
};

export default CompliancePage;
