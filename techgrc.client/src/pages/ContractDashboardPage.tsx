import React, { useEffect, useState } from "react";
import axios from "axios";
import { Grid, Paper, Typography, Button, CircularProgress, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { ContractDTO, SupplierDTO } from "../models/contractModels"; // Importiere die DTOs

const ContractDashboardPage: React.FC = () => {
    const [contracts, setContracts] = useState<ContractDTO[] | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchContracts = async () => {
            try {
                const response = await axios.get("/api/contracts");
                setContracts(response.data);
                setLoading(false);
            } catch (err) {
                setError("An error occurred while fetching contracts.");
                setLoading(false);
            }
        };

        fetchContracts();
    }, []);

    const handleNav = () => {
        window.location.href = "/contract";
    };

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
                   Dashboard
                </Typography>
            </Grid>

            <Grid item xs={12}>
                <Paper elevation={3} sx={{ padding: 2 }}>
                    <Typography variant="h6">Contracts List</Typography>
                    {contracts?.length === 0 && <Typography variant="body1">No contracts found.</Typography>}
                    <TableContainer component={Paper}>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Contract Title</TableCell>
                                    <TableCell>Supplier</TableCell>
                                    <TableCell>Start Date</TableCell>
                                    <TableCell>End Date</TableCell>
                                    <TableCell>Actions</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {contracts?.map((contract) => (
                                    <TableRow key={contract.contractId}>
                                        <TableCell>{contract.title}</TableCell>
                                        <TableCell>{contract.supplier.name}</TableCell>
                                        <TableCell>{new Date(contract.startDate).toLocaleDateString()}</TableCell>
                                        <TableCell>{new Date(contract.endDate).toLocaleDateString()}</TableCell>
                                        <TableCell>
                                            <Button variant="contained" color="primary" onClick={() => alert("Edit contract")}>
                                                Edit
                                            </Button>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            </Grid>

            <Grid item xs={12} mt={2}>
                <Button variant="contained" color="primary" onClick={() => alert('Create new contract')}>
                    Create New Contract
                </Button>
                <hr></hr>
                <Button variant="contained" color="primary" onClick={() => handleNav()}>
                    To Contract Management
                </Button>
            </Grid>
        </Grid>
    );
};

export default ContractDashboardPage;
