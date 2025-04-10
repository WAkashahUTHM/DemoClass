import React, { useState } from "react";
import { TextField, Button, Grid, Paper, Typography } from "@mui/material";
import { CreateContractDTO } from "../models/contractModels"; // Importiere DTOs

const CreateContractForm: React.FC = () => {
    const [formData, setFormData] = useState<CreateContractDTO>({
        title: "",
        description: "",
        startDate: "",
        endDate: "",
        supplierId: ""
    });

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value
        }));
    };

    const handleSubmit = async () => {
        try {
            const response = await axios.post("/api/contracts", formData);
            alert("Contract created successfully");
        } catch (error) {
            console.error("Failed to create contract", error);
        }
    };

    return (
        <Paper sx={{ padding: 2 }}>
            <Typography variant="h6">Create New Contract</Typography>
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        label="Contract Title"
                        variant="outlined"
                        fullWidth
                        name="title"
                        value={formData.title}
                        onChange={handleInputChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        label="Description"
                        variant="outlined"
                        fullWidth
                        name="description"
                        value={formData.description}
                        onChange={handleInputChange}
                    />
                </Grid>
                <Grid item xs={6}>
                    <TextField
                        label="Start Date"
                        type="date"
                        variant="outlined"
                        fullWidth
                        name="startDate"
                        value={formData.startDate}
                        onChange={handleInputChange}
                        InputLabelProps={{
                            shrink: true,
                        }}
                    />
                </Grid>
                <Grid item xs={6}>
                    <TextField
                        label="End Date"
                        type="date"
                        variant="outlined"
                        fullWidth
                        name="endDate"
                        value={formData.endDate}
                        onChange={handleInputChange}
                        InputLabelProps={{
                            shrink: true,
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        label="Supplier ID"
                        variant="outlined"
                        fullWidth
                        name="supplierId"
                        value={formData.supplierId}
                        onChange={handleInputChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button
                        variant="contained"
                        color="primary"
                        fullWidth
                        onClick={handleSubmit}
                    >
                        Create Contract
                    </Button>
                </Grid>
            </Grid>
        </Paper>
    );
};

export default CreateContractForm;
