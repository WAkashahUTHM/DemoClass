import React, { useState } from "react";
import axios from "axios";
import { 
    Box, 
    Container, 
    Typography, 
    Button, 
    CircularProgress, 
    TextField, 
    Paper, 
    List, 
    ListItem, 
    ListItemText 
} from "@mui/material";

// Define the interface for the message structure
interface Message {
    id: number;
    text: string;
    sender: 'user' | 'ai';
    timestamp: Date;
}

const GptDemoPage: React.FC = () => {
    // Handle interactivity state
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    // State for messages and input text
    const [messages, setMessages] = useState<Message[]>([]);
    const [inputText, setInputText] = useState<string>("");

    // Function to handle sending messages from user button
    const sendMessage = async () => {
        if (!inputText.trim()) return;

        const userMessage: Message = {
            id: Date.now(),
            text: inputText,
            sender: 'user',
            timestamp: new Date()
        };

        setMessages(prev => [...prev, userMessage]);
        setInputText("");
        setLoading(true);
        setError(null);

        try {
            // Replace with your actual API endpoint
            const response = await axios.post('/api/chat', {
                message: inputText
            });

            const aiMessage: Message = {
                id: Date.now() + 1,
                text: response.data.response || "Sorry, I do not understand.",
                sender: 'ai',
                timestamp: new Date()
            };

            setMessages(prev => [...prev, aiMessage]);
        } catch (err) {
            const errorMessage: Message = {
                id: Date.now() + 2,
                text: "Sorry, I couldn't process your request. Please try again.",
                sender: 'ai',
                timestamp: new Date()
            };

            // Remove thinking message and add error response
            setMessages(prev => [...prev, errorMessage]);
            setError("Failed to get AI response. Please try again.");
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    // Function to handle sending message on Enter key press
    const handleKeyPress = (e: React.KeyboardEvent) => {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            sendMessage();
        }
    };


    return (
        <Box>
            <Container maxWidth="md">
                <Typography variant="h4" gutterBottom>
                    Chat GPT Demo Page
                </Typography>

                {/* Chat Window */}
                <Paper elevation={3} sx={{ height: 400, mb: 2, display: 'flex', flexDirection: 'column' }}>
                    <Box sx={{ flex: 1, overflow: 'auto', p: 1 }}>
                        <List>
                            {messages.map((message) => (
                                <ListItem 
                                    key={message.id}
                                    sx={{
                                        justifyContent: message.sender === 'user' ? 'flex-end' : 'flex-start',
                                        mb: 1
                                    }}
                                >
                                    <Paper
                                        elevation={1}
                                        sx={{
                                            p: 2,
                                            maxWidth: '70%',
                                            backgroundColor: message.sender === 'user' ? '#1976d2' : '#f5f5f5',
                                            color: message.sender === 'user' ? 'white' : 'black'
                                        }}
                                    >
                                        <ListItemText
                                            primary={ message.text }
                                            secondary={
                                                <Typography 
                                                    variant="caption" 
                                                    sx={{ 
                                                        color: message.sender === 'user' ? 'rgba(255,255,255,0.7)' : 'text.secondary' 
                                                    }}
                                                >
                                                    {message.sender === 'user' ? 'You' : 'AI'} • {message.timestamp.toLocaleTimeString()}
                                                </Typography>
                                            }
                                        />
                                    </Paper>
                                </ListItem>
                            ))}
                            {loading && (
                                <ListItem sx={{ justifyContent: 'flex-start' }}>
                                    <Paper elevation={1} sx={{ p: 2, backgroundColor: '#f5f5f5' }}>
                                        <CircularProgress size={20} sx={{ mr: 1 }} />
                                        <Typography variant="body2" component="span">AI is typing...</Typography>
                                    </Paper>
                                </ListItem>
                            )}
                        </List>
                    </Box>
                </Paper>

                {/* Error Display */}
                {error && (
                    <Typography color="error" sx={{ mb: 2 }}>
                        {error}
                    </Typography>
                )}

                {/* Input Area */}
                <Box sx={{ display: 'flex', gap: 1 }}>
                    <TextField
                        fullWidth
                        multiline
                        maxRows={3}
                        value={inputText}
                        onChange={(e) => setInputText(e.target.value)}
                        onKeyPress={handleKeyPress}
                        placeholder="Type your message here..."
                        disabled={loading}
                        variant="outlined"
                    />
                    <Button 
                        variant="contained" 
                        color="primary" 
                        onClick={sendMessage}
                        disabled={loading || !inputText.trim()}
                        sx={{ minWidth: 80 }}
                    >
                        Send
                    </Button>
                </Box>
            </Container>
        </Box>
    );
};

export default GptDemoPage;
