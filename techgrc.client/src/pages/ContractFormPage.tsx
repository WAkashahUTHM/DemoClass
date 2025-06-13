import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from "axios";
import { 
    Box, 
    Container, 
    Typography, 
    Paper, 
    Button,
    CircularProgress,
    Alert
} from '@mui/material';
import { ContractDTO, SupplierDTO } from "../models/contractModels"; // Importiere die DTOs

const ContractFormPage: React.FC = () => {
    const { id } = useParams<{ id: string }>(); // Get ID from URL
    const [supplierInfo, setSupplierInfo] = useState<SupplierDTO | null>(null);
    const [contractInfo, setContractInfo] = useState<ContractDTO | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const handleNav = (path: string) => {
        window.location.href = path;
    }

    useEffect(() => {
        const fetchContractData = async () => {
            if (!id) {
                setError("No contract ID provided");
                setLoading(false);
                return;
            }

            try {
                setLoading(true);
                setError(null);

                // Fetch contract data from your API
                const response = await axios.get(`/api/contracts/${id}`);
                const contract: ContractDTO = response.data;
                
                setContractInfo(contract);
                setSupplierInfo(contract.supplier);
                
            } catch (err) {
                console.error('Error fetching contract:', err);
                setError("Failed to load contract data. Please try again.");
            } finally {
                setLoading(false);
            }
        };

        fetchContractData();
    }, [id]); // Re-run when ID changes

    if (loading) {
        return (
            <Container maxWidth="lg" sx={{ py: 4, display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '50vh' }}>
                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: 2 }}>
                    <CircularProgress size={40} />
                    <Typography variant="h6">Loading contract data...</Typography>
                </Box>
            </Container>
        );
    }

    if (error) {
        return (
            <Container maxWidth="lg" sx={{ py: 4 }}>
                <Alert severity="error">
                    <Typography variant="body1">
                        <strong>Error: </strong>{error}
                    </Typography>
                </Alert>
            </Container>
        );
    }

    return (
        <Container maxWidth="lg" sx={{ py: 4 }}>
            <Typography variant="h3" component="h1" gutterBottom fontWeight="bold">
                Contract & Supply Information
            </Typography>
            
            {/* Supplier Information Section */}
            <Paper elevation={3} sx={{ p: 3, mb: 4 }}>
                <Typography variant="h4" component="h2" gutterBottom color="primary">
                    Supplier Information
                </Typography>
                {supplierInfo && (
                    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography variant="body1" component="span" fontWeight="bold" sx={{ minWidth: 150 }}>
                                Supplier Name:
                            </Typography>
                            <Typography variant="body1" component="span" sx={{ ml: 1 }}>
                                {supplierInfo.name}
                            </Typography>
                        </Box>
                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography variant="body1" component="span" fontWeight="bold" sx={{ minWidth: 150 }}>
                                Category:
                            </Typography>
                            <Typography variant="body1" component="span" sx={{ ml: 1 }}>
                                {supplierInfo.category}
                            </Typography>
                        </Box>
                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography variant="body1" component="span" fontWeight="bold" sx={{ minWidth: 150 }}>
                                Contact Info:
                            </Typography>
                            <Typography variant="body1" component="span" sx={{ ml: 1 }}>
                                {supplierInfo.contactInfo}
                            </Typography>
                        </Box>
                    </Box>
                )}
            </Paper>

            {/* Contract Information Section */}
            <Paper elevation={3} sx={{ p: 3 }}>
                <Typography variant="h4" component="h2" gutterBottom color="success.main">
                    Contract Information
                </Typography>
                {contractInfo && (
                    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography variant="body1" component="span" fontWeight="bold" sx={{ minWidth: 180 }}>
                                Contract Title:
                            </Typography>
                            <Typography variant="body1" component="span" sx={{ ml: 1 }}>
                                {contractInfo.title}
                            </Typography>
                        </Box>
                        <Box sx={{ display: 'flex', alignItems: 'flex-start' }}>
                            <Typography variant="body1" component="span" fontWeight="bold" sx={{ minWidth: 180 }}>
                                Contract Description:
                            </Typography>
                            <Typography variant="body1" component="span" sx={{ ml: 1 }}>
                                {contractInfo.description}
                            </Typography>
                        </Box>
                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography variant="body1" component="span" fontWeight="bold" sx={{ minWidth: 180 }}>
                                Start Date:
                            </Typography>
                            <Typography variant="body1" component="span" sx={{ ml: 1 }}>
                                {contractInfo.startDate}
                            </Typography>
                        </Box>
                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography variant="body1" component="span" fontWeight="bold" sx={{ minWidth: 180 }}>
                                End Date:
                            </Typography>
                            <Typography variant="body1" component="span" sx={{ ml: 1 }}>
                                {contractInfo.endDate}
                            </Typography>
                        </Box>
                    </Box>
                )}
            </Paper>

            {/* Action Buttons */}
            <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
                <Button variant="contained" color="primary" size="large" onClick={ () => alert('Edit Contract')}>
                    Edit Contract
                </Button>
                <Button variant="contained" color="secondary" size="large" onClick={ () => alert('Printing Contract')}>
                    Print
                </Button>
                <Button variant="contained" color="success" size="large" onClick={ () => handleNav('/')}>
                    Back
                </Button>
            </Box>
        </Container>
    );
};

export default ContractFormPage;