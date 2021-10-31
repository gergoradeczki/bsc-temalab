import {
    Box,
    Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField
} from "@mui/material";
import React from "react";
import AddIcon from "@mui/icons-material/Add";
import {TodoState} from "./Todo";

interface NewTodoProps {
    onClick: (name: string, description: string, deadline: Date, state: TodoState) => void
}

interface NewTodoStates {
    isDialogOpen: boolean
    name?: string
    description?: string
}

class NewTodo extends React.Component<NewTodoProps, NewTodoStates> {
    constructor(props: any) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
        this.handleDialogClose = this.handleDialogClose.bind(this);
        this.handleDialogOK = this.handleDialogOK.bind(this);
        this.state = {
            isDialogOpen: false,
            name: "",
            description: ""
        }
    }

    handleDialogOK() {
        console.log('Clicked OK!');
        this.setState({
            isDialogOpen: false
        });
    }

    handleDialogClose() {
        this.setState({
            isDialogOpen: false
        });
    }

    handleChange(e: any) {
        const target = e.target;
        const value = target.checked;

        this.setState({
            isDialogOpen: true
        });
    }

    render() {
        return (
            <>
                <Button variant="contained" endIcon={<AddIcon />} sx={{width: 275}} onClick={this.handleChange}>
                    Új TODO
                </Button>
                <Dialog open={this.state.isDialogOpen} onClose={this.handleDialogClose}>
                    <DialogTitle>Új TODO felvétele</DialogTitle>
                    <DialogContent>
                        <DialogContentText>Add meg az új TODO adatait.</DialogContentText>
                        <Box component="form" sx={{'& > :not(style)': { m: 1, width: '25ch' },}}>
                            <TextField
                                id="todo-name"
                                label="Név"
                                variant="outlined"
                                onChange={(e) => this.setState({name: e.target.value})}
                            />
                            <TextField
                                id="todo-description"
                                label="Leírás"
                                variant="outlined"
                                onChange={(e) => this.setState({description: e.target.value})}
                            />
                        </Box>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleDialogClose}>Cancel</Button>
                        <Button onClick={this.handleDialogClose}>Subscribe</Button>
                    </DialogActions>
                </Dialog>
            </>
        )
    }
}

export { NewTodo }