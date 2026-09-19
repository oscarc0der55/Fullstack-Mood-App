import {useState} from 'react';
import {getCurrentUser, loginWithCookie} from '../connection/AuthConnection';
import {useNavigate} from 'react-router-dom';

export default function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    async function handleSubmit(e){
        e.preventDefault();
        await loginWithCookie(email, password);
        const user = await getCurrentUser();

    if (user.role === 'Admin') {
        navigate('/admin-home', { replace: true });
    } else {
        navigate('/user-home', { replace: true });
    }
    }

    return (
        <form onSubmit={handleSubmit}>
            <input type ="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" required />
            <input type ="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" required />
            <button type="submit">Login</button>
        </form>
    )
}